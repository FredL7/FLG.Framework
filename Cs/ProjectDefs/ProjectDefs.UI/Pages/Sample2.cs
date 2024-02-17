using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public class Sample2 : IPage {
    private const string _pageId = "Sample2";

    public string GetPageId() => _pageId;

    private string _layoutId = "";
    public string GetLayoutId() => _layoutId;
    public void SetLayoutId(string layoutId) { _layoutId = layoutId; }

    public void Setup()
    {
        var factory = Locator.Instance.Get<IUIFactory>();

        var page2test1 = factory.ProxyLayoutElement("page2-test-1");
        var page2test2 = factory.ProxyLayoutElement("page2-test-2");
        var page2test3 = factory.ProxyLayoutElement("page2-test-3");

        var ui = Locator.Instance.Get<IUIManager>();
        var layout = ui.GetLayout(_layoutId);
        var target = layout.GetTarget("content");
        target.AddChild(page2test1, _pageId);
        target.AddChild(page2test2, _pageId);
        target.AddChild(page2test3, _pageId);

        // TODO add the above to the layout under the target "content"
    }
}
