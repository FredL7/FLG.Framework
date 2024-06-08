using System.Xml;

using FLG.Cs.Datamodel;
using FLG.Cs.IO;
using FLG.Cs.Math;
using FLG.Cs.UI.Grids;
using FLG.Cs.UI.Layouts;
using FLG.Cs.Validation;


using File = FLG.Cs.IO.File;


namespace FLG.Cs.UI {
    internal class XMLParser {
        private string[] _uiDirs;

        private Dictionary<string, IPage> _pages;
        private Dictionary<string, Layout> _components;
        private Dictionary<string, Layout> _layouts;
        private Dictionary<string, AbstractLayoutElement> _targets;

        private List<File> _pageXMLFiles;
        private List<File> _layoutFiles;

        private ILogManager _logger;

        public Dictionary<string, IPage> GetPages() => _pages;
        public Dictionary<string, Layout> GetLayouts() => _layouts;

        internal XMLParser(string[] uiDirs, ILogManager logger)
        {
            _uiDirs = uiDirs;

            _pages = new();
            _components = new();
            _layouts = new();
            _targets = new();

            _layoutFiles = new();
            foreach (string dir in _uiDirs)
                _layoutFiles.AddRange(IOUtils.GetFilePathsByExtension(dir, ".layout"));

            _pageXMLFiles = new();
            foreach( string dir in _uiDirs)
                _pageXMLFiles .AddRange(IOUtils.GetFilePathsByExtension(dir, ".page"));

            _logger = logger;
        }

        internal Result Parse()
        {
            foreach(string dir in _uiDirs)
                if (!Directory.Exists(dir))
                    return new Result($"{Path.GetFullPath(dir)} does not exists");

            return ParsePages();
        }

        #region Pages
        private Result ParsePages()
        {
            if (_pageXMLFiles.Count == 0)
            {
                return new Result("No file with extension .page found");
            }

            foreach (var file in _pageXMLFiles)
            {
                _logger.Debug($"Begin Parsing XML {file.filename}");

                Result result = ValidateXml(file, "page", out XmlDocument _, out XmlNode? rootNode);
                if (!result || rootNode == null) return result;
                result = ValidatePageXml(rootNode, out string binding, out string layoutId, out XmlNode? layoutNode);
                if (!result || binding == string.Empty || layoutId == string.Empty || layoutNode == null) return result;

                result = LoadPageLayout(layoutId);
                if (!result) return result;

                result = InstantiatePageCs(binding, layoutId);
                if (!result) return result;

                result = ParsePage(layoutId, layoutNode, binding);
                if (!result) return result;

                _logger.Debug($"Finished Parsing XML {file.filename}");
            }

            return Result.SUCCESS;
        }

        private Result InstantiatePageCs(string binding, string layoutId)
        {
            IPage? page;
            /*try
            {
                // var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var assemblyFolder = Path.GetDirectoryName(typeof(XMLParser).Assembly.Location);
                _logger.Debug($"FRED: {assemblyFolder}");
                if (assemblyFolder == null) return new Result("Could not locate current assembly directory");

                var assembly = System.Reflection.Assembly.LoadFile(Path.Combine(assemblyFolder, "ProjectDefs.UI.dll"));
                if (assembly == null) return new Result("Could not load ProjectsDefs.UI.dll");

                var type = assembly.GetType(binding);
                if (type == null) return new Result($"Could not instantiate a class of type {binding}: type {binding} not found");

                var pageObject = assembly.CreateInstance(binding);
                if (pageObject == null) return new Result($"Could not instantiate a class of type {binding}: result is null");

                page = pageObject as IPage;
                _logger.Debug($"Instantiated IPage of type {binding}");
            }
            catch (Exception e)
            {
                return new Result($"Could not instantiate a class of type {binding}: {e}");
            }*/

            try
            {
                var type = Type.GetType(binding + ", ProjectDefs.UI");
                if (type == null) return new Result($"Could not instantiate a class of type {binding}: type {binding} not found");

                var pageObject = Activator.CreateInstance(type);
                if (pageObject == null) return new Result($"Could not instantiate a class of type {binding}: could not create an instance of type {type}");

                page = pageObject as IPage;
                _logger.Debug($"Instantiated IPage of type {binding}");
            }
            catch (Exception e)
            {
                return new Result($"Could not instantiate a class of type {binding}: {e}");
            }

            if (page == null) return new Result($"Could not instantiate a class of type {binding}: result is null");
            page.LayoutId = layoutId;
            _pages.Add(page.PageId, page);

            return Result.SUCCESS;
        }

        private Result LoadPageLayout(string layoutId)
        {
            _logger.Debug($"Begin Parsing layout with id {layoutId}");
            Result result = LoadLayout(layoutId);
            if (!result) return result;
            _logger.Debug($"Finished Parsing layout with id {layoutId}");

            if (!_layouts.ContainsKey(layoutId))
                _layouts.Add(layoutId, _components[layoutId]);

            return Result.SUCCESS;
        }

        private Result ParsePage(string layoutId, XmlNode layoutNode, string pageId)
        {
            Layout layout = _layouts[layoutId];
            Result result = GetTargetNodes(layout, layoutNode, out List<XmlNode> targetNodes, out List<ILayoutElement> targetElements);
            if (!result) return result;
            for (int i = 0; i < targetNodes.Count; ++i)
            {
                result = ConvertNodeRecursiveForTarget(targetNodes[i], targetElements[i], targetElements[i].Name, pageId);
                if (!result) return result;
            }

            return Result.SUCCESS;
        }

        private static Result ValidatePageXml(XmlNode rootNode, out string binding, out string layoutId, out XmlNode? layoutNode)
        {
            binding = string.Empty;
            layoutId = string.Empty;
            layoutNode = null;

            var childNodes = rootNode.ChildNodes;
            if (childNodes.Count != 2) return new Result("Root node must contain exactly 2 nodes, <binding> and <layout>");

            var firstChildNode = childNodes[0];
            if (firstChildNode?.Name != "binding") return new Result("First child of Root node must be named \"binding\"");
            binding = firstChildNode?.Attributes?["class"]?.Value ?? string.Empty;
            if (binding == string.Empty) return new Result($"<binding> node Does not have a \"class\" attribute");

            layoutNode = childNodes[1];
            if (layoutNode?.Name != "layout") return new Result("Second child of Root node must be named \"layout\"");
            layoutId = layoutNode?.Attributes?["id"]?.Value ?? string.Empty;
            if (layoutId == string.Empty) return new Result($"<layout> node Does not have an \"id\" attribute");

            return Result.SUCCESS;
        }

        private Result GetTargetNodes(Layout layout, XmlNode layoutNode, out List<XmlNode> targetNodes, out List<ILayoutElement> targetElements)
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
                    return new Result($"Layout {layout.Name} does not have a target with id=\"{targetId}\"");

                targetNodes.Add(targetNode);
                targetElements.Add(layout.GetTarget(targetId));
            }

            return Result.SUCCESS;
        }
        #endregion Pages

        #region Layouts
        private Result LoadLayout(string id)
        {
            if (_components.ContainsKey(id))
            {
                _logger.Debug($"Layout {id} already loaded OK");
                return Result.SUCCESS;
            }

            foreach (File file in _layoutFiles)
                if (file.file == id)
                    return ParseLayout(file, id);

            return new Result($"Layout with id {id} could not be found.");
        }

        private Result ParseLayout(File file, string id)
        {
            _logger.Debug($"Begin Parsing {file.filename}");
            Result result = ValidateXml(file, "layout", out XmlDocument _, out XmlNode? rootNode);
            if (!result || rootNode == null) return result;
            result = ValidateLayoutXml(rootNode, out XmlNode? rootChild);
            if (!result || rootChild == null) return result;

            result = ConvertNode(rootChild, out AbstractLayoutElement? root, id);
            if (!result || root == null) return result;

            result = ConvertNodeRecursive(rootChild, root);
            if (!result) return result;

            Container container = new("root", new LayoutAttributes());
            container.AddChild(root);

            Layout layout = new(container, id, _targets);
            _components.Add(id, layout);
            _logger.Debug($"Finished Parsing XML Layout {file.filename}");

            return Result.SUCCESS;
        }

        private Result ValidateLayoutXml(XmlNode rootNode, out XmlNode? rootChild)
        {
            rootChild = null;

            var childNodes = rootNode.ChildNodes;
            if (childNodes.Count != 1) return new Result("Root node must contain exactly 1 node");

            rootChild = childNodes[0];

            return Result.SUCCESS;
        }
        #endregion Layouts

        #region Conversion
        private Result ConvertNodeRecursive(XmlNode parentNode, AbstractLayoutElement parentLayoutElement)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                Result result = ConvertNode(node, out AbstractLayoutElement? layoutElement);
                if (!result || layoutElement == null) return result;

                parentLayoutElement.AddChild(layoutElement);

                if (layoutElement.IsTarget)
                {
                    /*if (node.HasChildNodes)
                        return new Result($"Target node {layoutElement.Name} cannot declare childrens");*/

                    _targets.Add(layoutElement.Name, layoutElement);
                }

                ConvertNodeRecursive(node, layoutElement);
            }

            return Result.SUCCESS;
        }

        private Result ConvertNodeRecursiveForTarget(XmlNode targetNode, ILayoutElement targetLayoutElement, string targetId, string pageId)
        {
            foreach (XmlNode node in targetNode.ChildNodes)
            {
                Result result = ConvertNode(node, out AbstractLayoutElement? layoutElement);
                if (!result || layoutElement == null) return result;

                targetLayoutElement.AddChild(layoutElement, pageId);

                if (layoutElement.IsTarget)
                {
                    if (node.HasChildNodes)
                        return new Result($"Target node {layoutElement.Name} cannot declare childrens");

                    _targets.Add(layoutElement.Name, layoutElement);
                }

                ConvertNodeRecursiveForTarget(node, layoutElement, targetId, pageId);
            }

            return Result.SUCCESS;
        }

        private Result ConvertNode(XmlNode node, out AbstractLayoutElement? convertedNode, string componentName = "")
        {
            var nodeType = node.Name;
            string name = GetNodeName(node, componentName);
            convertedNode = UIFactory.Xml(node, name);
            if (convertedNode == null)
            {
                // using UILibrary.Xml first => won't attempt to load a *.layout named after one of the concrete LayoutElement (HStack.layout for instance)
                if (!_components.ContainsKey(nodeType))
                {
                    Result result = LoadLayout(nodeType);
                    if (!result) return result;
                }
                convertedNode = (AbstractLayoutElement)_components[nodeType].Root;
            }

            return Result.SUCCESS;
        }
        #endregion Conversion

        #region Helpers
        #region XML
        private static Result ValidateXml(File file, string expectedRootName, out XmlDocument xmldoc, out XmlNode? rootNode)
        {
            xmldoc = new();
            rootNode = null;

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
            } // Technically optional

            return Result.SUCCESS;
        }

        private static string GetNodeName(XmlNode node, string externalFileName = "")
        {
            if (externalFileName != "")
            {
                return externalFileName;
            }

            return GetId(node);
        }

        internal static string GetId(XmlNode node) => GetStringAttribute(node, "id", node.Name);
        internal static string GetText(XmlNode node) => GetStringAttributeOrContent(node, "text");
        internal static Spacing GetMargin(XmlNode node) => GetSpacingAttribute(node, "margin");
        internal static Spacing GetPadding(XmlNode node) => GetSpacingAttribute(node, "padding");
        internal static float GetWidth(XmlNode node) => GetFloatAttribute(node, "width");
        internal static float GetHeight(XmlNode node) => GetFloatAttribute(node, "height");
        internal static int GetOrder(XmlNode node) => GetIntAttribute(node, "order");
        internal static float GetWeight(XmlNode node) => GetFloatAttribute(node, "weight", 1f);
        internal static EGridDirection GetDirection(XmlNode node) => GetDirectionAttribute(node);
        internal static EGridJustify GetJustify(XmlNode node) => GetJustifyAttribute(node);
        internal static EGridAlignment GetAlignment(XmlNode node) => GetAlignmentAttribute(node);
        internal static ETextAlignHorizontal GetTextAlignHorizontal(XmlNode node) => getTextAlignHorzAttribute(node);
        internal static ETextAlignVertical GetTextAlignVertical(XmlNode node) => getTextAlignVertAttribute(node);
        #endregion XML

        private static string GetAttributeValue(XmlNode node, string attr)
        {
            var value = node.Attributes?[attr]?.Value;
            if (value == null || value == string.Empty)
            {
                value = "";
            }
            return value;
        }

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
                if (value == null || value == string.Empty)
                {
                    return defaultValue;
                }
                else
                {
                    return value;
                }
            }
        }

        internal static string GetStringAttributeOrContent(XmlNode node, string attr, string defaultValue = "")
        {
            if (node.Attributes?[attr]?.Value != null)
            {
                var value = node?.Attributes[attr]?.Value;
                if (value != null && value != string.Empty)
                {
                    return value;
                }
            }
            else
            {
                var innerText = node.InnerText;
                if (innerText != null && innerText != string.Empty)
                {
                    return innerText;
                }
            }

            return defaultValue;
        }

        internal static int GetIntAttribute(XmlNode node, string attr, int defaultValue = 0)
        {
            var value = GetAttributeValue(node, attr);
            if (value == "")
            {
                return defaultValue;
            }
            else
            {
                return int.TryParse(value, out var x) ? x : defaultValue;
            }
        }

        internal static float GetFloatAttribute(XmlNode node, string attr, float defaultValue = 0f)
        {
            var value = GetAttributeValue(node, attr);
            if (value == "")
            {
                return defaultValue;
            }
            else
            {
                return float.TryParse(value, out var x) ? x : defaultValue;
            }
        }

        internal static Spacing GetSpacingAttribute(XmlNode node, string attr)
        {
            var value = GetAttributeValue(node, attr);
            if (value == "")
            {
                return Spacing.Zero;
            }
            else
            {
                var splittedValue = value.Split(' ');
                var intValues = Array.ConvertAll(splittedValue, x => int.TryParse(x, out var y) ? y : 0);
                return intValues.Length switch
                {
                    0 => Spacing.Zero,
                    1 => new Spacing(intValues[0]),
                    2 => new Spacing(intValues[0], intValues[1]),
                    3 => new Spacing(intValues[0], intValues[1], intValues[2]),
                    4 => new Spacing(intValues[0], intValues[1], intValues[2], intValues[3]),
                    _ => new Spacing(intValues[0], intValues[1], intValues[2], intValues[3]),
                };
            }
        }

        internal static EGridDirection GetDirectionAttribute(XmlNode node)
        {
            var value = GetAttributeValue(node, "direction");
            return EGridDirectionExtension.FromString(value);
        }

        internal static EGridJustify GetJustifyAttribute(XmlNode node)
        {
            var value = GetAttributeValue(node, "justify");
            return EGridJustifyExtension.FromString(value);
        }

        internal static EGridAlignment GetAlignmentAttribute(XmlNode node)
        {
            var value = GetAttributeValue(node, "align");
            return EGridAlignmentExtension.FromString(value);
        }

        internal static ETextAlignHorizontal getTextAlignHorzAttribute(XmlNode node)
        {
            var value = GetAttributeValue(node, "text-align");
            return ETextAlignHorizontalExtension.FromString(value);
        }

        internal static ETextAlignVertical getTextAlignVertAttribute(XmlNode node)
        {
            var value = GetAttributeValue(node, "text-align-vertical");
            return ETextAlignVerticalExtension.FromString(value);
        }
        #endregion Converters
        #endregion Helpers
    }
}
