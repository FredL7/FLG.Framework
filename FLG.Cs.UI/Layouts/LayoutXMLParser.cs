using System.Xml;

using FLG.Cs.Logger;
using FLG.Cs.Math;
using FLG.Cs.UI.Grid;

namespace FLG.Cs.UI.Layouts {
    internal static class LayoutXMLParser {
        internal static List<Layout>? Parse(string layoutDir)
        {
            if (!Directory.Exists(layoutDir))
            {
                LogManager.Instance.Error($"{Path.GetFullPath(layoutDir)} does not exists");
                return null;
            }

            var components = ParseForComponents(layoutDir);
            var layouts = ParseForLayouts(layoutDir, components);

            return layouts;
        }

        private static List<string> GetFilePathsByExtension(string dir, string extension)
        {
            var files = Directory.GetFiles(dir);
            List<string> result = new();
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == extension)
                    result.Add(Path.GetFullPath(file));
            }
            return result;
        }

        #region Components
        private static Dictionary<string, AbstractLayoutElement>? ParseForComponents(string layoutsDir)
        {
            Dictionary<string, AbstractLayoutElement> result = new();

            var componentFiles = GetFilePathsByExtension(layoutsDir, ".component");
            if (componentFiles.Count == 0)
            {
                LogManager.Instance.Debug($"Layout dir ({Path.GetFullPath(layoutsDir)}) does not contain any components (*.component)");
                return null;
            }

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
                LogManager.Instance.Error("XML root node not found");
                return null;
            }

            var rootChildCount = rootNode.ChildNodes.Count;
            if (rootChildCount == 0)
            {
                LogManager.Instance.Error("XML root node must have a child node");
                return null;
            }
            else if (rootChildCount > 1)
            {
                LogManager.Instance.Error("XML root node can only have one child node");
                return null;
            }

            var rootChildNode = rootNode.ChildNodes[0];
            if (rootChildNode == null)
                return null;

            var result = ConvertNode(rootChildNode, components, Path.GetFileNameWithoutExtension(filepath));
            if (result == null)
                return null;

            ConvertRecursive(rootChildNode, result, components);
            return result;
        }
        #endregion Components

        #region Layouts
        private static List<Layout>? ParseForLayouts(string layoutsDir, Dictionary<string, AbstractLayoutElement>? components)
        {
            List<Layout> result = new();

            List<string> layoutFiles = GetFilePathsByExtension(layoutsDir, ".layout");
            if (layoutFiles.Count == 0)
            {
                LogManager.Instance.Error($"Layout dir ({Path.GetFullPath(layoutsDir)}) does not contain any layouts (*.layout)");
                return null;
            }

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
                LogManager.Instance.Error("XML root node not found");
                return null;
            }

            var rootChildCount = rootNode.ChildNodes.Count;
            if (rootChildCount == 0)
            {
                LogManager.Instance.Error("XML root node must have a child node");
                return null;
            }
            else if (rootChildCount > 1)
            {
                LogManager.Instance.Error("XML root node can only have one child node");
                return null;
            }

            var rootChildNode = rootNode.ChildNodes[0];
            if (rootChildNode == null)
                return null;

            var convertedRootChildNode = ConvertNode(rootChildNode, components);
            if (convertedRootChildNode == null)
                return null;

            Layout layout = new(convertedRootChildNode, Path.GetFileNameWithoutExtension(filepath));
            ConvertRecursive(rootChildNode, convertedRootChildNode, components);
            return layout;
        }
        #endregion Layouts

        #region XML
        private static void ConvertRecursive(XmlNode parentNode, AbstractLayoutElement parentLayoutElement, Dictionary<string, AbstractLayoutElement>? components)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                AbstractLayoutElement? layoutElement = ConvertNode(node, components);
                if (layoutElement != null)
                {
                    parentLayoutElement.AddChild(layoutElement);
                    ConvertRecursive(node, layoutElement, components);
                }
            }
        }

        private static AbstractLayoutElement? ConvertNode(XmlNode node, Dictionary<string, AbstractLayoutElement>? components, string componentName = "")
        {
            var nodeType = node.Name;
            var name = componentName == "" ? GetName(node) : componentName;
            switch(nodeType)
            {
                case "HStack": return new HStack(node, name);
                case "VStack": return new VStack(node, name);
                case "ProxyLayoutElement": return new ProxyLayoutElementLeaf(node, name); // TODO: tmp
                default:
                    if (components != null && components.ContainsKey(name))
                        return components[name];
                    return null;
            }
        }

        internal static string GetName(XmlNode node) => GetStringAttribute(node, "name", node.Name);
        internal static Spacing GetMargin(XmlNode node) => GetSpacingAttribute(node, "margin");
        internal static Spacing GetPadding(XmlNode node) => GetSpacingAttribute(node, "padding");
        internal static float GetWidth(XmlNode node) => GetFloatAttribute(node, "width");
        internal static float GetHeight(XmlNode node) => GetFloatAttribute(node, "height");
        internal static int GetOrder(XmlNode node) => GetIntAttribute(node, "order");
        internal static float GetWeight(XmlNode node) => GetFloatAttribute(node, "weight", 1f);
        internal static EGridDirection GetDirection(XmlNode node) => GetDirectionAttribute(node);
        internal static EGridJustify GetJustify(XmlNode node) => GetJustifyAttribute(node);
        internal static EGridAlignment GetAlignment(XmlNode node) => GetAlignmentAttribute(node);
        #endregion XML

        #region Converters
        private static string GetStringAttribute(XmlNode node, string attr, string defaultValue = "")
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return defaultValue;
            }
            else
            {
                var value = node.Attributes[attr].Value;
                if (value == "")
                {
                    return defaultValue;
                }
                else
                {
                    return value;
                }
            }
        }

        private static int GetIntAttribute(XmlNode node, string attr, int defaultValue = 0)
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return defaultValue;
            }
            else
            {
                var value = node.Attributes[attr].Value;
                if (value == "")
                {
                    return defaultValue;
                }
                else
                {
                    return int.TryParse(value, out var x) ? x : defaultValue;
                }
            }
        }

        private static float GetFloatAttribute(XmlNode node, string attr, float defaultValue = 0f)
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return defaultValue;
            }
            else
            {
                var value = node.Attributes[attr].Value;
                if (value == "")
                {
                    return defaultValue;
                }
                else
                {
                    return float.TryParse(value, out var x) ? x : defaultValue;
                }
            }
        }

        private static Spacing GetSpacingAttribute(XmlNode node, string attr)
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return Spacing.Zero;
            }
            else
            {
                var value = node.Attributes[attr].Value;
                if (value == "")
                {
                    return Spacing.Zero;
                }
                else
                {
                    if (value == "")
                        return Spacing.Zero;

                    var splittedValue = value.Split(' ');
                    var intValues = Array.ConvertAll(splittedValue, x => int.TryParse(x, out var y) ? y : 0);
                    switch (intValues.Length)
                    {
                        case 0:
                            return Spacing.Zero;
                        case 1:
                            return new Spacing(intValues[0]);
                        case 2:
                            return new Spacing(intValues[0], intValues[1]);
                        case 3:
                            return new Spacing(intValues[0], intValues[1], intValues[2]);
                        case 4:
                            return new Spacing(intValues[0], intValues[1], intValues[2], intValues[3]);
                        default:
                            LogManager.Instance.Warn("Spacing attribute has too many values");
                            return new Spacing(intValues[0], intValues[1], intValues[2], intValues[3]);
                    }
                }
            }
        }

        private static EGridDirection GetDirectionAttribute(XmlNode node)
        {
            if (node.Attributes?["direction"]?.Value == null)
            {
                return EGridDirectionExtension.FromString("");
            }
            else
            {
                var value = node.Attributes["direction"].Value;
                if (value == "")
                {
                    return EGridDirectionExtension.FromString("");
                }
                else
                {
                    return EGridDirectionExtension.FromString(value);
                }
            }
        }

        private static EGridJustify GetJustifyAttribute(XmlNode node)
        {
            if (node.Attributes?["justify"]?.Value == null)
            {
                return EGridJustifyExtension.FromString("");
            }
            else
            {
                var value = node.Attributes["justify"].Value;
                if (value == "")
                {
                    return EGridJustifyExtension.FromString("");
                }
                else
                {
                    return EGridJustifyExtension.FromString(value);
                }
            }
        }

        private static EGridAlignment GetAlignmentAttribute(XmlNode node)
        {
            if (node.Attributes?["justify"]?.Value == null)
            {
                return EGridAlignmentExtension.FromString("");
            }
            else
            {
                var value = node.Attributes["justify"].Value;
                if (value == "")
                {
                    return EGridAlignmentExtension.FromString("");
                }
                else
                {
                    return EGridAlignmentExtension.FromString(value);
                }
            }
        }
        #endregion Converters
    }
}
