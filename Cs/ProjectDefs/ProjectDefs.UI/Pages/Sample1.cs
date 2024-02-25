using FLG.Cs.IDatamodel;

public class Sample1 : IPage {
    private const string PAGE_ID = "Sample1";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup()
    {
        // Defined in sample1.page
    }
}
