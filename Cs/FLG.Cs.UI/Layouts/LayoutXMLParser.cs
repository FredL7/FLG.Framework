using System.Xml;

using FLG.Cs.Logger;
using FLG.Cs.IO;
using FLG.Cs.ServiceLocator;

namespace FLG.Cs.UI.Layouts {
    internal static class LayoutXMLParser {
        internal static List<Layout>? Parse(string layoutDir)
        {
            if (!Directory.Exists(layoutDir))
            {
                Locator.Instance.Get<ILogManager>().Error($"{Path.GetFullPath(layoutDir)} does not exists");
                return null;
            }

            var components = ParseForComponents(layoutDir);
            var layouts = ParseForLayouts(layoutDir, components);

            return layouts;
        }

        #region Components
        private static Dictionary<string, AbstractLayoutElement>? ParseForComponents(string layoutsDir)
        {
            var componentFiles = IOUtils.GetFilePathsByExtension(layoutsDir, ".component");
            if (componentFiles.Count == 0)
            {
                Locator.Instance.Get<ILogManager>().Debug($"Layout dir ({Path.GetFullPath(layoutsDir)}) does not contain any components (*.component)");
                return null;
            }

            Dictionary<string, AbstractLayoutElement> result = new(componentFiles.Count);
            foreach (var file in componentFiles)
            {
                var component = ParseComponent(file, result); // TODO: component within component might throw error (order of load)
                if (component != null)
                    result.Add(component.GetName(), component);
            }

            return result;
        }

        private static AbstractLayoutElement? ParseComponent(string filepath, Dictionary<string, AbstractLayoutElement>? components)
        {
            XmlDocument xmldoc = new();
            xmldoc.Load(filepath); // TODO: Could fail here (wrap in try-catch?)

            var rootNode = xmldoc.DocumentElement;
            if (rootNode == null)
            {
                Locator.Instance.Get<ILogManager>().Error("XML root node not found");
                return null;
            }

            var rootChildCount = rootNode.ChildNodes.Count;
            if (rootChildCount == 0)
            {
                Locator.Instance.Get<ILogManager>().Error("XML root node must have a child node");
                return null;
            }
            else if (rootChildCount > 1)
            {
                Locator.Instance.Get<ILogManager>().Error("XML root node can only have one child node");
                return null;
            }

            var rootChildNode = rootNode.ChildNodes[0];
            if (rootChildNode == null)
                return null;

            var result = XMLParser.ConvertNode(rootChildNode, components, Path.GetFileNameWithoutExtension(filepath));
            if (result == null)
                return null;

            XMLParser.ConvertRecursive(rootChildNode, result, components);
            return result;
        }
        #endregion Components

        #region Layouts
        private static List<Layout>? ParseForLayouts(string layoutsDir, Dictionary<string, AbstractLayoutElement>? components)
        {
            List<string> layoutFiles = IOUtils.GetFilePathsByExtension(layoutsDir, ".layout");
            if (layoutFiles.Count == 0)
            {
                Locator.Instance.Get<ILogManager>().Error($"Layout dir ({Path.GetFullPath(layoutsDir)}) does not contain any layouts (*.layout)");
                return null;
            }

            List<Layout> result = new(layoutFiles.Count);
            foreach (var file in layoutFiles)
            {
                var layout = ParseLayout(file, components);
                if (layout != null)
                    result.Add(layout);
            }

            return result;
        }

        private static Layout? ParseLayout(string filepath, Dictionary<string, AbstractLayoutElement>? components)
        {
            XmlDocument xmldoc = new();
            xmldoc.Load(filepath); // TODO: Could fail here (wrap in try-catch?)

            var rootNode = xmldoc.DocumentElement;
            if (rootNode == null)
            {
                Locator.Instance.Get<ILogManager>().Error("XML root node not found");
                return null;
            }

            var rootChildCount = rootNode.ChildNodes.Count;
            if (rootChildCount == 0)
            {
                Locator.Instance.Get<ILogManager>().Error("XML root node must have a child node");
                return null;
            }
            else if (rootChildCount > 1)
            {
                Locator.Instance.Get<ILogManager>().Error("XML root node can only have one child node");
                return null;
            }

            var rootChildNode = rootNode.ChildNodes[0];
            if (rootChildNode == null)
                return null;

            var convertedRootChildNode = XMLParser.ConvertNode(rootChildNode, components);
            if (convertedRootChildNode == null)
                return null;

            Layout layout = new(convertedRootChildNode, Path.GetFileNameWithoutExtension(filepath));
            if (convertedRootChildNode.GetIsTarget())
                layout.AddTarget(convertedRootChildNode);
            XMLParser.ConvertRecursive(rootChildNode, convertedRootChildNode, components, layout);
            return layout;
        }
        #endregion Layouts
    }
}
