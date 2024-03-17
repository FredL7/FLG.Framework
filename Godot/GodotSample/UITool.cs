using Godot;
using System;
using System.Collections.Generic;

using FLG.Cs.Framework;
using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Godot.Helpers;
using FLG.Godot.UI;

using sysV2 = System.Numerics.Vector2;
using gdV2 = Godot.Vector2;
using flgLabel = FLG.Godot.UI.Label;
using flgButton = FLG.Godot.UI.Button;


[Tool]
public partial class UITool : Control, IUIObserver {
    private const string LOGS_RELATIVE_PATH = "../../_logs"; // TODO: Move to serialized field to appear in the inspector?
    private const string LAYOUTS_RELATIVE_PATH = "../../Cs/ProjectDefs/ProjectDefs.UI/Layouts";
    private const string PAGES_RELATIVE_PATH = "../../Cs/ProjectDefs/ProjectDefs.UI/Pages";


    private Size _window = new(1920, 1080);

    private IUIManager _uiManager;

    private Dictionary<string, Node> _layouts = new();
    private Dictionary<string, List<Node>> _pages = new();
    private string _currentLayout = string.Empty;
    private string _currentPage = string.Empty;

    public override void _Ready()
    {
        base._Ready();

        if (Engine.IsEditorHint())
        {
            InitializeFramework();
            SetupUI();
        }
    }

    public void SetupUI()
    {
        _uiManager = Locator.Instance.Get<IUIManager>();
        Console.WriteLine(_uiManager);

        Clear();
        DrawUI();

        _uiManager.AddObserver(this);
    }

    public override void _ExitTree()
    {
        _uiManager.RemoveObserver(this);
        base._ExitTree();
    }

    public void OnCurrentPageChanged(string pageId, string layoutId)
    {
        if (_currentPage != pageId)
        {
            foreach (var page in _pages)
                foreach (var pageItem in page.Value)
                    pageItem.Set("visible", false);

            foreach (var pageItem in _pages[pageId])
                pageItem.Set("visible", true);

            if (_currentLayout != layoutId)
            {
                foreach (var layout in _layouts)
                    layout.Value.Set("visible", false);
                _layouts[layoutId].Set("visible", true);
            }
        }
    }

    private void InitializeFramework()
    {
        Preferences prefs = new();
        FrameworkManager.Instance.InitializeFramework(prefs);

        PreferencesLogs prefsLogs = new()
        {
            logsDir = LOGS_RELATIVE_PATH,
        };
        FrameworkManager.Instance.InitializeLogs(prefsLogs);

        PreferencesUI prefsUI = new()
        {
            layoutsDir = ProjectSettings.GlobalizePath("res://" + LAYOUTS_RELATIVE_PATH),
            pagesDir = ProjectSettings.GlobalizePath("res://" + PAGES_RELATIVE_PATH),
            windowSize = _window
        };
        FrameworkManager.Instance.InitializeUI(prefsUI);
    }

    private void Clear()
    {
        SceneHelper.RemoveAllChildrensImmediately(this);
    }

    private void DrawUI()
    {
        DrawLayouts();
    }

    private Node AddNode(string name, ILayoutElement layoutElement, Node parent)
    {
        var position = layoutElement.Position;
        var dimensions = layoutElement.Dimensions;

        return AddNode(name, position, dimensions, parent);
    }

    private Node AddNode(string name, sysV2 position, Size dimensions, Node parent)
    {
        Control node = new()
        {
            Name = name,
            Position = new gdV2(position.X, position.Y),
            Size = new gdV2(dimensions.Width, dimensions.Height),
        };
        parent.AddChild(node);
        node.Owner = GetTree().EditedSceneRoot;
        return node;
    }

    private void DrawLayouts()
    {
        foreach (var layout in _uiManager.GetLayouts())
            DrawLayout(layout);
    }

    private void DrawLayout(ILayout layout)
    {
        string id = layout.Name;
        var root = layout.Root;
        var layoutNode = AddNode("layout " + id, root, this);
        _layouts.Add(id, layoutNode);
        DrawLayoutRecursive(layoutNode, root);
    }

    private void DrawLayoutRecursive(Node parentNode, ILayoutElement layoutElementParent)
    {
        var targets = layoutElementParent.GetTargets();
        foreach (var target in targets)
        {
            if (layoutElementParent.HasChildren(target))
            {
                var parentForAddNode = parentNode;
                if (target != ILayoutElement.DEFAULT_CHILDREN_TARGET)
                {
                    var targetNode = AddNode(target, sysV2.Zero, layoutElementParent.Dimensions, parentNode);

                    if (!_pages.ContainsKey(target))
                        _pages.Add(target, new List<Node>());
                    _pages[target].Add(targetNode);

                    targetNode.Set("visible", false);
                    parentForAddNode = targetNode;
                }

                foreach (ILayoutElement child in layoutElementParent.GetChildrens(target))
                {
                    var node = DrawNode(child, parentForAddNode);
                    DrawLayoutRecursive(node, child);
                }
            }
        }
    }

    private Node DrawNode(ILayoutElement layoutElement, Node parentNode)
    {
        Node node;
        var root = GetTree().EditedSceneRoot;
        var fromEditor = Engine.IsEditorHint();
        bool parentSetter = true;
        switch (layoutElement.Type)
        {
            case ELayoutElement.BUTTON:
                IWidget<IButton> btn = new flgButton((IButton)layoutElement);
                node = btn.Draw(parentNode, fromEditor);
                break;
            case ELayoutElement.LABEL:
                IWidget<ILabel> label = new flgLabel((ILabel)layoutElement);
                node = label.Draw(parentNode, fromEditor);
                break;
            case ELayoutElement.SPRITE:
                IWidget<ISprite> sprite = new Sprite((ISprite)layoutElement);
                node = sprite.Draw(parentNode, fromEditor);
                break;
            case ELayoutElement.TEXT:
                IWidget<IText> text = new Text((IText)layoutElement);
                node = text.Draw(parentNode, fromEditor);
                break;
            default:
                node = AddNode(layoutElement.Name + " (" + layoutElement.GetType() + ")", layoutElement, parentNode);
                parentSetter = false;
                break;
        }

        if (parentSetter)
        {
            parentNode.AddChild(node);
            node.Owner = root;
        }

        return node;
    }
}
