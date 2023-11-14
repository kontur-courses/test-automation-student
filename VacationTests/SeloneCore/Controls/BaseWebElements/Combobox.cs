using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using NUnit.Framework;
using SeloneCore.Props;

namespace SeloneCore.Controls.BaseWebElements;

public class Combobox : ControlBase
{
    private readonly Input input;

    public Combobox(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
        input = controlFactory.CreateControl<Input>(Container.Search(x =>
            x.XPath(".//*[contains(@data-comp-name,'CommonWrapper Input')]")));
        MenuItems = controlFactory.CreateElementsCollection<Button>(
            Container.Root(),
            x => x.WithTid("ComboBoxMenu__item").FixedByIndex()
        );
    }

    public IProp<string> Text => Container.Text();
    public IProp<bool> HasError => Container.HasError();
    public ElementsCollection<Button> MenuItems { get; }

    public void SelectValue(string value)
    {
        Open();
        MenuItems.Wait().Single(x => x.Text, Contains.Substring(value)).Click();
    }

    public void InputValue(string text)
    {
        Container.Click();
        input.ClearAndInputText(text);
    }

    public void WaitValue(string text)
    {
        input.Value.Wait().EqualTo(text);
    }

    public void Open()
    {
        Container.Click();
    }
}