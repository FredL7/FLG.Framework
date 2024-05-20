using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public class Sample1 : IPage {
    private const string PAGE_ID = "Sample1";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup(IUIManager ui, IUIFactory factory)
    {
        var btn = factory.Button("page1-test-button", "Click Me!", OnBtnClicked, new(order: 3, width: 128, height: 40, margin: new(0, 0, 0, 20)));

        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(btn, PageId);
    }

    public void OnBtnClicked()
    {
        var _uiManager = Locator.Instance.Get<IUIManager>();
        _uiManager.SetCurrentPage("Sample2");
    }

    public void OnOpen() { }
    public void OnClose() { }
}
