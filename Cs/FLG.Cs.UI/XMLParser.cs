using FLG.Cs.Logger;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Grid;
using FLG.Cs.UI.Layouts;
using System.Xml;

namespace FLG.Cs.UI {
    internal static class XMLParser {
        #region XML
        // TODO Separate into 2 distinct methods, one with adding layout targets, the other without
        internal static void ConvertRecursive(XmlNode parentNode, AbstractLayoutElement parentLayoutElement, Dictionary<string, AbstractLayoutElement>? components = null, Layout? layout = null)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                AbstractLayoutElement? layoutElement = ConvertNode(node, components);
                if (layoutElement != null)
                {
                    parentLayoutElement.AddChild(layoutElement);
                    if (layout != null && layoutElement.GetIsTarget())
                        layout.AddTarget(layoutElement);
                    ConvertRecursive(node, layoutElement, components, layout);
                }
            }
        }

        internal static AbstractLayoutElement? ConvertNode(XmlNode node, Dictionary<string, AbstractLayoutElement>? components = null, string componentName = "")
        {
            var nodeType = node.Name;
            var targetName = GetTarget(node);
            var nodeName = GetName(node);
            var defaultName = nodeName;
            // If node name is its default value, try to overwrite it by the target name
            if (nodeName == nodeType && targetName != string.Empty)
                defaultName = targetName;
            var name = componentName == "" ? defaultName : componentName;
            switch (nodeType)
            {
                case "HStack": return new HStack(name, node);
                case "VStack": return new VStack(name, node);
                case "ProxyLayoutElement": return new ProxyLayoutElementLeaf(name, node); // TODO: tmp
                default:
                    if (components != null && components.ContainsKey(name))
                        return components[name];
                    return null;
            }
        }

        internal static string GetName(XmlNode node) => GetStringAttribute(node, "name", node.Name);
        internal static string GetTarget(XmlNode node) => GetStringAttribute(node, "target", String.Empty);
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
    }
}
