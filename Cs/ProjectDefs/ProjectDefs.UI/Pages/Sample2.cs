using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public class Sample2 : IPage {
    private const string PAGE_ID = "Sample2";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup()
    {
        var factory = Locator.Instance.Get<IUIFactory>();

        var page2test1 = factory.ProxyLayoutElement("page2-test-1");
        var page2test2 = factory.ProxyLayoutElement("page2-test-2");
        var page2test3 = factory.ProxyLayoutElement("page2-test-3");

        var ui = Locator.Instance.Get<IUIManager>();
        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(page2test1, PageId);
        target.AddChild(page2test2, PageId);
        target.AddChild(page2test3, PageId);

        // TODO add the above to the layout under the target "content"
    }
}
