using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public class Sample2 : IPage {
    private const string PAGE_ID = "Sample2";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    private IText _text;
    private int _index = 0;

    public void Setup()
    {
        var factory = Locator.Instance.Get<IUIFactory>();

        var proxy = factory.ProxyLayoutElement("page2-test-1", new());
        var label = factory.Label("page2-test-label", "Hello World!", new());
        var sprite = factory.Sprite("page2-test-sprite", "icon.svg", new());
        var btn = factory.Button("page2-test-button", "Click Me!", OnBtnClicked, new());
        _text = (IText)factory.Text("page2-test-text", "BBCode 1: [img]icon.svg[/img]", new());

        var ui = Locator.Instance.Get<IUIManager>();
        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(proxy, PageId);
        target.AddChild(label, PageId);
        target.AddChild(sprite, PageId);
        target.AddChild(btn, PageId);
        target.AddChild(_text, PageId);
    }

    public void OnBtnClicked()
    {
        var _uiManager = Locator.Instance.Get<IUIManager>();
        _uiManager.SetCurrentPage("Sample1");
    }
}
