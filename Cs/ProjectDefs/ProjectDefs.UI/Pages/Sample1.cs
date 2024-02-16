using FLG.Cs.IDatamodel;

public class Sample1 : IPage {
    private const string _id = "Sample1";
    public string GetID() => _id;

    public void Setup()
    {
        // Defined in sample1.page
    }
}
