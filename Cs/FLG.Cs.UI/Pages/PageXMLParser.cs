using System.Xml;

using FLG.Cs.IO;
using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI.Pages {
    internal static class PageXMLParser {
        internal static List<Page>? Parse(string pagesDir)
        {
            if (!Directory.Exists(pagesDir))
            {
                Locator.Instance.Get<ILogManager>().Error($"{Path.GetFullPath(pagesDir)} does not exists");
                return null;
            }

            var pages = ParseForPages(pagesDir);
            return pages;
        }

        private static List<Page>? ParseForPages(string pagesDir)
        {
            List<string> pagesFiles = IOUtils.GetFilePathsByExtension(pagesDir, ".page");
            if (pagesFiles.Count == 0)
            {
                Locator.Instance.Get<ILogManager>().Error($"Page dir ({Path.GetFullPath(pagesDir)}) does not contain any pages (*.page)");
                return null;
            }

            List<Page> pages = new(pagesFiles.Count);
            foreach (var file in pagesFiles)
            {
                var page = ParsePage(file);
                if (page != null)
                    pages.Add(page);
            }

            return pages;
        }

        private static Page? ParsePage(string filepath)
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

            var layoutId = GetLayoutId(rootNode);
            if (layoutId == null)
                return null;

            var targets = ParseTargets(rootNode);
            if (targets == null)
            {
                Locator.Instance.Get<ILogManager>().Debug($"No targets defined in page {Path.GetFileNameWithoutExtension(filepath)}");
                return null;
            }

            Page page = new(layoutId, Path.GetFileNameWithoutExtension(filepath), targets); // TODO: Add targets
            return page;
        }

        private static string? GetLayoutId(XmlNode root)
        {
            var layoutNode = root.SelectSingleNode("layout");
            if (layoutNode == null)
            {
                Locator.Instance.Get<ILogManager>().Error("Could not find <layout> node");
                return null;
            }
            return layoutNode.InnerText;
        }

        private static Dictionary<string, List<AbstractLayoutElement>>? ParseTargets(XmlNode root)
        {
            var targetNodes = root.SelectNodes("target");
            if (targetNodes == null || targetNodes.Count == 0)
                return null;

            Dictionary<string, List<AbstractLayoutElement>> result = new(targetNodes.Count);
            foreach (XmlNode targetNode in targetNodes)
            {
                var targetid = XMLParser.GetStringAttribute(targetNode, "id", string.Empty);
                if (targetid == string.Empty)
                {
                    Locator.Instance.Get<ILogManager>().Error("Could not find target id in <target> node");
                    return null;
                }

                if (result.ContainsKey(targetid))
                {
                    Locator.Instance.Get<ILogManager>().Error($"Page already contains content for id={targetid}");
                    return null;
                }

                if (targetNode.ChildNodes.Count == 0)
                {
                    Locator.Instance.Get<ILogManager>().Error($"No element in target with id=\"{targetid}\"");
                    return null;
                }

                List<AbstractLayoutElement> layoutElements = new(targetNode.ChildNodes.Count);
                foreach(XmlNode child in targetNode.ChildNodes)
                {
                    AbstractLayoutElement? layoutElement = XMLParser.ConvertNode(child); // TODO: specify AbstractLayoutElementLead (vs Composite in Layouts?)
                    if (layoutElement == null)
                        return null;

                    XMLParser.ConvertRecursive(child, layoutElement);
                    layoutElements.Add(layoutElement);
                }

                result.Add(targetid, layoutElements);
            }

            return result;
        }
    }
}
