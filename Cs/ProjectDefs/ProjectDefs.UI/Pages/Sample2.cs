using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public class Sample2 : IPage {
    private const string PAGE_ID = "Sample2";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup()
    {
        var factory = Locator.Instance.Get<IUIFactory>();

        var page2test1 = factory.ProxyLayoutElement("page2-test-1", new());
        var label = factory.Label("page2-test-label", "Hello World!", new());
        var sprite = factory.Sprite("page2-test-sprite", "FLG.Godot.UI/Spritesheets/spritesheet-cards-alpha.png", new());
        var btn = factory.Button("page2-test-button", "Click Me!", OnBtnClicked, new());
        var text = factory.Text("page2-test-text", "My Cards: [img region=0,0,64,64]FLG.Godot.UI/SpriteSheets/spritesheet-cards-alpha.png[/img]", new());

        var ui = Locator.Instance.Get<IUIManager>();
        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(page2test1, PageId);
        target.AddChild(label, PageId);
        target.AddChild(sprite, PageId);
        target.AddChild(btn, PageId);
        target.AddChild(text, PageId);
    }

    public void OnBtnClicked()
    {
        var logger = Locator.Instance.Get<ILogManager>();
        logger.Info("OnBtnClicked");
    }
}
