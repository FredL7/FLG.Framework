using FLG.Cs.IDatamodel;

public class Sample1 : IPage {
    private const string _pageId = "Sample1";

    public string GetPageId() => _pageId;

    private string _layoutId = "";
    public string GetLayoutId() => _layoutId;
    public void SetLayoutId(string layoutId) { _layoutId = layoutId; }

    public void Setup()
    {
        // Defined in sample1.page
    }
}
