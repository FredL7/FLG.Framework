using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public class Sample1 : IPage {
    private const string PAGE_ID = "Sample1";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup()
    {
        var factory = Locator.Instance.Get<IUIFactory>();
        var btn = factory.Button("page1-test-button", "Click Me!", OnBtnClicked, new() { Order=4, Width=128, Height=40, Margin=new(0, 0, 0, 20) });

        var ui = Locator.Instance.Get<IUIManager>();
        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(btn, PageId);
    }

    public void OnBtnClicked()
    {
        var _uiManager = Locator.Instance.Get<IUIManager>();
        _uiManager.SetCurrentPage("Sample2");
    }
}
