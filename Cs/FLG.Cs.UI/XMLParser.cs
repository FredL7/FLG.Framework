using FLG.Cs.IO;
using FLG.Cs.Logger;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Grid;
using FLG.Cs.UI.Layouts;
using FLG.Cs.Validation;
using System.Xml;

using File = FLG.Cs.IO.File;


namespace FLG.Cs.UI {
    internal class XMLParser {
        private string _layoutsDir;
        private Dictionary<string, Layout> _pages;
        private Dictionary<string, Layout> _layouts;

        private List<File> _pageFiles;
        private List<File> _layoutFiles;

        ILogManager _log;

        public Dictionary<string, Layout> GetPages() => _pages;

        internal XMLParser(string layoutsDir)
        {
            _layoutsDir = layoutsDir;

            _pages = new();
            _layouts = new();

            _pageFiles = IOUtils.GetFilePathsByExtension(_layoutsDir, ".page");
            _layoutFiles = IOUtils.GetFilePathsByExtension(_layoutsDir, ".layout");

            _log = Locator.Instance.Get<ILogManager>();
        }

        internal Result Parse()
        {
            if (!Directory.Exists(_layoutsDir))
                return new Result($"{Path.GetFullPath(_layoutsDir)} does not exists");

            return ParsePages();
        }

        #region *.page
        private Result ParsePages()
        {
            if (_pageFiles.Count == 0)
            {
                return new Result("No file with extension .page found");
            }

            foreach (var file in _pageFiles)
            {
                _log.Debug($"Begin Parsing XML {file.filename}");
                Result result = ParsePage(file);
                if (!result) return result;
                _log.Debug($"Finished Parsing XML {file.filename}");
            }

            return Result.SUCCESS;
        }

        private Result ParsePage(File file)
        {
            Result result = ValidateXml(file, "page", out XmlDocument _, out XmlNode? rootNode, out XmlNode? layoutNode);
            if (!result || rootNode == null || layoutNode == null) return result;
            result = ValidateXmlPage(layoutNode, out string layoutId);
            if (!result || layoutId == string.Empty) return result;

            _log.Debug($"Begin Parsing layout with id {layoutId}");
            result = LoadLayout(layoutId);
            if (!result) return result;
            _log.Debug($"Finished Parsing layout with id {layoutId}");
            var layout = _layouts[layoutId];

            result = GetTargetNodes(layout, layoutNode, out List<XmlNode> targetNodes, out List<AbstractLayoutElement> targetElements);
            if (!result) return result;
            for (int i = 0; i < targetNodes.Count; ++i)
            {
                result = ConvertNodeRecursiveForTarget(targetNodes[i], targetElements[i], targetElements[i].GetName());
                if (!result) return result;
            }

            // Watch out for reusable vs copy content
            _pages.Add(file.file, layout);
            return Result.SUCCESS;
        }

        private static Result ValidateXmlPage(XmlNode layoutNode, out string layoutId)
        {
            layoutId = string.Empty;
            if (layoutNode?.Name != "layout")
                return new Result($"Root node must contain a child node named \"layout\"");

            layoutId = layoutNode?.Attributes?["id"]?.Value ?? string.Empty;
            if (layoutId == string.Empty)
                return new Result($"Layout node Does not have an \"id\" attribute");

            return Result.SUCCESS;
        }

        private Result GetTargetNodes(Layout layout, XmlNode layoutNode, out List<XmlNode> targetNodes, out List<AbstractLayoutElement> targetElements)
        {
            targetNodes = new();
            targetElements = new();

            foreach (XmlNode targetNode in layoutNode.ChildNodes)
            {
                if (targetNode.Name != "target")
                    return new Result($"Unhandled node type: {targetNode.Name}");

                string targetId = targetNode.Attributes?["id"]?.Value ?? string.Empty;
                if (targetId == string.Empty)
                    return new Result("Target node is missing the \"id\" attribute");

                if (!layout.HasTarget(targetId))
                    return new Result($"Layout {layout.GetName()} does not have a target with id=\"{targetId}\"");

                targetNodes.Add(targetNode);
                targetElements.Add(layout.GetTarget(targetId));
            }

            return Result.SUCCESS;
        }
        #endregion *.page

        #region *.layout
        private Result LoadLayout(string id)
        {
            if (_layouts.ContainsKey(id))
            {
                _log.Debug($"Layout {id} already loaded OK");
                return Result.SUCCESS;
            }

            foreach (File file in _layoutFiles)
                if (file.file == id)
                    return ParseLayout(file, id);

            return new Result($"Layout with id {id} could not be found, searching for {id}.layout in {_layoutsDir}.");
        }

        private Result ParseLayout(File file, string id)
        {
            _log.Debug($"Begin Parsing {file.filename}");
            Result result = ValidateXml(file, "layout", out XmlDocument _, out XmlNode? rootNode, out XmlNode? rootChild);
            if (!result || rootNode == null || rootChild == null) return result;

            result = ConvertNode(rootChild, out AbstractLayoutElement? root, id);
            if (!result || root == null) return result;

            Dictionary<string, AbstractLayoutElement> targets = new();
            result = ConvertNodeRecursive(rootChild, root, targets);
            if (!result) return result;

            Layout layout = new(root, id, targets);
            _layouts.Add(id, layout);
            _log.Debug($"Finished Parsing XML Layout {file.filename}");
            return Result.SUCCESS;
        }
        #endregion *.layout

        #region Conversion
        private Result ConvertNodeRecursive(XmlNode parentNode, AbstractLayoutElement parentLayoutElement, Dictionary<string, AbstractLayoutElement> targets)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                Result result = ConvertNode(node, out AbstractLayoutElement? layoutElement);
                if (!result || layoutElement == null) return result;

                parentLayoutElement.AddChild(layoutElement);

                if (layoutElement.GetIsTarget())
                {
                    if (node.HasChildNodes)
                        return new Result($"Target node {layoutElement.GetName()} cannot declare childrens");

                    targets.Add(layoutElement.GetName(), layoutElement);
                }

                ConvertNodeRecursive(node, layoutElement, targets);
            }

            return Result.SUCCESS;
        }

        private Result ConvertNodeRecursiveForTarget(XmlNode targetNode, AbstractLayoutElement targetLayoutElement, string targetId)
        {
            foreach (XmlNode node in targetNode.ChildNodes)
            {
                Result result = ConvertNode(node, out AbstractLayoutElement? layoutElement);
                if (!result || layoutElement == null) return result;

                targetLayoutElement.AddChild(layoutElement, targetId);

                if (layoutElement.GetIsTarget())
                {
                    /*
                    if (node.HasChildNodes)
                        return new Result($"Target node {layoutElement.GetName()} cannot declare childrens");
                    */

                    return new Result("Cannot declare targets within *.page <target> nodes");
                    // TODO: Will eventually want to add targets within pages for widgets (or find some other way)
                }

                ConvertNodeRecursiveForTarget(node, layoutElement, targetId);
            }

            return Result.SUCCESS;
        }

        private Result ConvertNode(XmlNode node, out AbstractLayoutElement? convertedNode, string componentName = "")
        {
            var nodeType = node.Name;
            string name = GetNodeName(node, componentName);
            convertedNode = UILibrary.Xml(node, name);
            if (convertedNode == null)
            {
                // using UILibrary.Xml first => won't attempt to load a *.layout named after one of the concrete LayoutElement (HStack.layout for instance)
                if (!_layouts.ContainsKey(nodeType))
                {
                    Result result = LoadLayout(nodeType);
                    if (!result) return result;
                }
                convertedNode = (AbstractLayoutElement)_layouts[nodeType].GetRoot(); // TODO: Way without cast?
            }

            return Result.SUCCESS;
        }
        #endregion Conversion

        #region Helpers
        #region XML
        private static Result ValidateXml(File file, string expectedRootName, out XmlDocument xmldoc, out XmlNode? rootNode, out XmlNode? rootChild)
        {
            xmldoc = new();
            rootNode = null;
            rootChild = null;

            try
            {
                xmldoc.Load(file.fullpath);
            }
            catch (Exception e)
            {
                return new Result($"XML file could not be read: {file} - {e.Message}");
            }

            rootNode = xmldoc.DocumentElement;
            if (rootNode == null)
            {
                return new Result($"XML root node not found: {file}");
            }

            if (rootNode.Name != expectedRootName)
            {
                return new Result($"XML root node Should be named {expectedRootName}: {file}");
            }

            var rootChildCount = rootNode.ChildNodes.Count;
            rootChild = rootNode.ChildNodes.Item(0);
            if (rootChildCount == 0 || rootChild == null)
            {
                return new Result($"XML root node must have a child node named: {file}");
            }
            else if (rootChildCount > 1)
            {
                return new Result($"XML root node can only have one child node: {file}");
            }

            return Result.SUCCESS;
        }

        private static string GetNodeName(XmlNode node, string externalFileName = "")
        {
            /*
             * If the node is the root of an external file, use that file's name;
             * Then if the node has a name attribute, use that value;
             * Then if the node has a target attribute, use that value;
             * Then return the XmlNode Name (which is the fallback value for GetName(node);
             */
            var targetName = GetTarget(node);
            var nodeName = GetName(node);
            if (externalFileName != "")
            {
                return externalFileName;
            }
            else if (nodeName != node.Name)
            {
                return nodeName;
            }
            else if (targetName != "")
            {
                return targetName;
            }
            else
            {
                return nodeName;
            }
        }

        internal static string GetName(XmlNode node) => GetStringAttribute(node, "name", node.Name);
        internal static string GetTarget(XmlNode node) => GetStringAttribute(node, "target", string.Empty);
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
        internal static string GetStringAttribute(XmlNode node, string attr, string defaultValue = "")
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return defaultValue;
            }
            else
            {
                var value = node?.Attributes[attr]?.Value;
                if (value == null || value == "")
                {
                    return defaultValue;
                }
                else
                {
                    return value;
                }
            }
        }

        internal static int GetIntAttribute(XmlNode node, string attr, int defaultValue = 0)
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return defaultValue;
            }
            else
            {
                var value = node?.Attributes[attr]?.Value;
                if (value == null || value == "")
                {
                    return defaultValue;
                }
                else
                {
                    return int.TryParse(value, out var x) ? x : defaultValue;
                }
            }
        }

        internal static float GetFloatAttribute(XmlNode node, string attr, float defaultValue = 0f)
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return defaultValue;
            }
            else
            {
                var value = node?.Attributes[attr]?.Value;
                if (value == null || value == "")
                {
                    return defaultValue;
                }
                else
                {
                    return float.TryParse(value, out var x) ? x : defaultValue;
                }
            }
        }

        internal static Spacing GetSpacingAttribute(XmlNode node, string attr)
        {
            if (node.Attributes?[attr]?.Value == null)
            {
                return Spacing.Zero;
            }
            else
            {
                var value = node?.Attributes[attr]?.Value;
                if (value == null || value == "")
                {
                    return Spacing.Zero;
                }
                else
                {
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
                            Locator.Instance.Get<ILogManager>().Warn("Spacing attribute has too many values");
                            return new Spacing(intValues[0], intValues[1], intValues[2], intValues[3]);
                    }
                }
            }
        }

        internal static EGridDirection GetDirectionAttribute(XmlNode node)
        {
            if (node.Attributes?["direction"]?.Value == null)
            {
                return EGridDirectionExtension.FromString("");
            }
            else
            {
                var value = node?.Attributes["direction"]?.Value;
                if (value == null || value == "")
                {
                    return EGridDirectionExtension.FromString("");
                }
                else
                {
                    return EGridDirectionExtension.FromString(value);
                }
            }
        }

        internal static EGridJustify GetJustifyAttribute(XmlNode node)
        {
            if (node.Attributes?["justify"]?.Value == null)
            {
                return EGridJustifyExtension.FromString("");
            }
            else
            {
                var value = node?.Attributes["justify"]?.Value;
                if (value == null || value == "")
                {
                    return EGridJustifyExtension.FromString("");
                }
                else
                {
                    return EGridJustifyExtension.FromString(value);
                }
            }
        }

        internal static EGridAlignment GetAlignmentAttribute(XmlNode node)
        {
            if (node.Attributes?["justify"]?.Value == null)
            {
                return EGridAlignmentExtension.FromString("");
            }
            else
            {
                var value = node?.Attributes["justify"]?.Value;
                if (value == null || value == "")
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
        #endregion Helpers
    }
}
