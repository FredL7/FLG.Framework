using Godot;

using FLG.Cs.Logger;
using FLG.Godot.Helpers;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Factory;
using FLG.Cs.UI;
using FLG.Cs.UI.Layouts;

namespace FLG.Godot.UI {
    [Tool]
    public partial class UITool : Control {
        private const string LOGS_RELATIVE_PATH = "../../_logs";
        private const string LAYOUTS_RELATIVE_PATH = "UI/Layouts";
        private const string PAGES_RELATIVE_PATH = "UI/Pages";

        private IUIManager _uiManager;

        public override void _Ready()
        {
            base._Ready();

            if (Engine.IsEditorHint())
            {
                SceneHelper.RemoveAllChildrensImmediately(this);
                InitializeFramework();
                LoadUI();
                GenerateUI();
            }
        }

        private void InitializeFramework()
        {

            var logsPath = ProjectSettings.GlobalizePath("res://" + LOGS_RELATIVE_PATH);
            LogManager.Instance.SetLogLocation(logsPath);
            ManagerFactory.CreateUIManager();
            _uiManager = Locator.Instance.Get<IUIManager>();
        }

        private void LoadUI()
        {
            var layoutsPath = ProjectSettings.GlobalizePath("res://" + LAYOUTS_RELATIVE_PATH);
            var pagesPath = ProjectSettings.GlobalizePath("res://" + PAGES_RELATIVE_PATH);
            _uiManager.RegisterLayouts(layoutsPath);
            _uiManager.RegisterPages(pagesPath);
        }

        private void GenerateUI()
        {
            foreach (var layout in _uiManager.GetLayouts())
                GenerateLayout(layout);
        }

        private void GenerateLayout(ILayout layout)
        {
            Control layoutNode = new()
            {
                Name = "Layout " + layout.GetName()
            };
            AddNodePersist(layoutNode);

            var root = layout.GetRoot();
            GenerateUIRecursive(layoutNode, root);
        }

        private void AddNodePersist(Node node)
        {
            AddChild(node);
            node.Owner = GetTree().EditedSceneRoot;
        }

        private void GenerateUIRecursive(Node parentNode, ILayoutElement layoutElementParent)
        {
            // TODO
        }
    }
}
