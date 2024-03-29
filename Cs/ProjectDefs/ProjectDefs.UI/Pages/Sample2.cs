using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public class Sample2 : IPage {
    private const string PAGE_ID = "Sample2";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup(IUIManager ui, IUIFactory factory)
    {
        var label = factory.Label("page2-test-label", "Hello World!", new() { Width = 128, Height = 40, Margin = new(0, 0, 0, 20) }, new());
        var sprite = factory.Sprite("page2-test-sprite", "icon.svg", new() { Width = 128, Height = 128, Margin = new(0, 0, 0, 20) });
        var btn = factory.Button("page2-test-button", "Click Me!", OnBtnClicked, new() { Width = 128, Height = 40, Margin = new(0, 0, 0, 20) });
        var text = (IText)factory.Text(
            "page2-test-text", "BBCode: [img width=40 height=40]icon.svg[/img]",
            new() { Width = 128, Height = 40 },
            new() { AlignHorizontal = ETextAlignHorizontal.RIGHT }
        );

        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(label, PageId);
        target.AddChild(sprite, PageId);
        target.AddChild(btn, PageId);
        target.AddChild(text, PageId);
    }

    public void OnBtnClicked()
    {
        var _uiManager = Locator.Instance.Get<IUIManager>();
        _uiManager.SetCurrentPage("Sample1");
    }

    public void OnOpen() { }
    public void OnClose() { }
}
