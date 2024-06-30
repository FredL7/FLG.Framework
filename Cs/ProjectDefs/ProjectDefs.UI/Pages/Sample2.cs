using FLG.Cs.Datamodel;
using FLG.Cs.Model;
using FLG.Cs.ServiceLocator;

public enum Form1Items { FIRSTNAME, LASTNAME }
public static class Form1ItemsExtension {
    public static string ToLabel(this Form1Items item)
    {
        return item switch
        {
            Form1Items.FIRSTNAME => "Firstname",
            Form1Items.LASTNAME => "Lastname",
            _ => "",
        };
    }
}

public class Sample2 : IPage {
    private const string PAGE_ID = "Sample2";

    public string PageId { get => PAGE_ID; }
    public string LayoutId { get; set; } = "";

    public void Setup(IUIManager ui, IUIFactory factory)
    {
        // Basic Fields
        var label = factory.Label("page2-test-label", "Hello World!", new(width: 128, height: 40, margin: new(0, 0, 0, 20)), new());
        var sprite = factory.Sprite("page2-test-sprite", "icon.svg", new(width: 128, height: 128, margin: new(0, 0, 0, 20)));
        var btn = factory.Button("page2-test-button", "Click Me!", OnBtnClicked, new(width: 128, height: 40, margin: new(0, 0, 0, 20)));
        var text = (IText)factory.Text(
            "page2-test-text", "BBCode: [img width=40 height=40]icon.svg[/img]",
            new(width: 128, height: 40, margin: new (0, 0, 0, 20)),
            new(alignHorizontal: ETextAlignHorizontal.RIGHT)
        );

        // Form
        var formFields = new List<IInputField>() {
            factory.InputField("page2-test-form-item-firsname", Form1Items.FIRSTNAME.ToLabel(), "my first name", new SimpleStringModel(), new()),
            factory.InputField("page2-test-form-item-lastname", Form1Items.LASTNAME.ToLabel(), "my first name", new SimpleStringModel(), new())
        };
        var form = factory.Form("page2-test-form", "Test Form", formFields, OnForm1Submit, new (width: 500, height: 100), new());

        // Setup Layout
        var layout = ui.GetLayout(LayoutId);
        var target = layout.GetTarget("content");
        target.AddChild(label, PageId);
        target.AddChild(sprite, PageId);
        target.AddChild(btn, PageId);
        target.AddChild(text, PageId);
        target.AddChild(form, PageId);
    }

    private void OnBtnClicked()
    {
        var ui = Locator.Instance.Get<IUIManager>();
        ui.SetCurrentPage("Sample1");
    }

    private void OnForm1Submit(string name, IFormModel model)
    {
        var logger = Locator.Instance.Get<ILogManager>();
        string firstname = model.GetItem(Form1Items.FIRSTNAME.ToLabel()).GetValueAsString();
        string lastname = model.GetItem(Form1Items.LASTNAME.ToLabel()).GetValueAsString();
        logger.Debug($"Form {name} Submitted with values:\nFirstname={firstname}, Lastname={lastname}");
    }

    public void OnRegister() { }
    public void OnOpen() { }
    public void OnClose() { }
}
