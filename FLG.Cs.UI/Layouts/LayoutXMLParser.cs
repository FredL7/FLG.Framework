﻿using FLG.Cs.Math;
using FLG.Cs.UI.Grid;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal class LayoutXMLParser {
        private bool _error = false;
        private string _errorMsg = String.Empty;
        internal bool IsValid {  get => !_error; }
        internal string ErrorMsg { get => _errorMsg; }

        internal Layout Parse(string layoutPath)
        {
            ResetError();

            XmlDocument xmlDoc = new();
            xmlDoc.Load(layoutPath);

            var layoutNode = xmlDoc.DocumentElement;
            if (layoutNode == null) {
                Error("Top-level node must be <Layout>");
                return new NullLayout();
            }

            var rootCount = layoutNode.ChildNodes.Count;
            var rootNode = layoutNode.ChildNodes[0];
            if (rootCount == 0 || rootNode == null)
            {
                Error("<layout> must have a child node");
                return new NullLayout();
            }
            else if (rootCount > 1)
            {
                Error("<layout> can only have 1 child (root)");
                return new NullLayout();
            }

            var rootLayoutElement = ParseNode(rootNode);
            if (_error)
            {
                return new NullLayout();
            }
            //?: Root must be Composite?
            Layout layout = new(rootLayoutElement);
            ParseRecursive(rootNode, rootLayoutElement);

            return layout;
        }

        private void ParseRecursive(XmlNode parentNode, AbstractLayoutElement parentLayoutElement)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                AbstractLayoutElement layoutElement = ParseNode(node);
                parentLayoutElement.AddChild(layoutElement);
                ParseRecursive(node, layoutElement);
            }
        }

        private AbstractLayoutElement ParseNode(XmlNode node)
        {
            var name = node.Name;
            switch(name)
            {
                case "HStack":
                    return new HStack(node);
                case "VStack":
                    return new VStack(node);
                case "ProxyLayoutElement":
                    return new ProxyLayoutElementLeaf(node);
                default:
                    Error($"Unrecognized node {name}");
                    return new NullLayoutElement();
            }
        }

        internal static Spacing GetMargin(XmlNode node) => GetSpacingAttribute(node, "margin");
        internal static Spacing GetPadding(XmlNode node) => GetSpacingAttribute(node, "padding");
        internal static float GetWidth(XmlNode node) => GetFloatAttribute(node, "width");
        internal static float GetHeight(XmlNode node) => GetFloatAttribute(node, "height");
        internal static int GetOrder(XmlNode node) => GetIntAttribute(node, "order");
        internal static float GetWeight(XmlNode node) => GetFloatAttribute(node, "weight", 1f);
        internal static EGridDirection GetDirection(XmlNode node) => GetDirectionAttribute(node);
        internal static EGridJustify GetJustify(XmlNode node) => GetJustifyAttribute(node);
        internal static EGridAlignment GetAlignment(XmlNode node) => GetAlignmentAttribute(node);

        #region Converters
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
                            // TODO: Log too many values
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

        #region Error
        private void ResetError()
        {
            _error = false;
            _errorMsg = String.Empty;
        }

        private void Error(string msg)
        {
            _error = true;
            _errorMsg = msg;
        }
        #endregion Error
    }
}
