using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

public enum Form1Items { FIRSTNAME, LASTNAME }
public static class Form1ItemsExtension {
    public static string ToLabel(this Form1Items item)
    {
        switch (item)
        {
            case Form1Items.FIRSTNAME: return "Firstname";
            case Form1Items.LASTNAME: return "Lastname";
            default: return "";
        }
    }
}

public class Sample2 : IPage {
    private const string PAGE_ID = "Sample2";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup(IUIManager ui, IUIFactory factory)
    {
        // Basic Fields
        var label = factory.Label("page2-test-label", "Hello World!", new() { Width = 128, Height = 40, Margin = new(0, 0, 0, 20) }, new());
        var sprite = factory.Sprite("page2-test-sprite", "icon.svg", new() { Width = 128, Height = 128, Margin = new(0, 0, 0, 20) });
        var btn = factory.Button("page2-test-button", "Click Me!", OnBtnClicked, new() { Width = 128, Height = 40, Margin = new(0, 0, 0, 20) });
        var text = (IText)factory.Text(
            "page2-test-text", "BBCode: [img width=40 height=40]icon.svg[/img]",
            new() { Width = 128, Height = 40 },
            new() { AlignHorizontal = ETextAlignHorizontal.RIGHT }
        );

        // Individual Inputs
        var inputfield = factory.InputField("page2-test-inputfield", "Input Field", "my placeholder", new SimpleStringModel(), new() { Width = 128, Height = 40 });

        // Form
        var formFields = new List<IInputField>() {
            factory.InputField("page2-test-form-item-firsname", Form1Items.FIRSTNAME.ToLabel(), "my first name", new SimpleStringModel(), new()),
            factory.InputField("page2-test-form-item-lastname", Form1Items.LASTNAME.ToLabel(), "my first name", new SimpleStringModel(), new())
        };
        var form = factory.Form("page2-test-form", formFields, OnForm1Submit, new LayoutAttributes());

        // Setup Layout
        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(label, PageId);
        target.AddChild(sprite, PageId);
        target.AddChild(btn, PageId);
        target.AddChild(text, PageId);
        target.AddChild(inputfield, PageId);
        target.AddChild(form, PageId);
    }

    public void OnBtnClicked()
    {
        var ui = Locator.Instance.Get<IUIManager>();
        ui.SetCurrentPage("Sample1");
    }

    public void OnForm1Submit()
    {
        /*
        string firstname = _formModel.GetItem(Form1Items.FIRSTNAME.ToLabel()).GetValueAsString();
        string lastname = _formModel.GetItem(Form1Items.LASTNAME.ToLabel()).GetValueAsString();
        var logger = Locator.Instance.Get<ILogManager>();
        logger.Debug($"Firstname={firstname}, Lastname={lastname}");
        */
    }

    public void OnOpen() { }
    public void OnClose() { }
}
