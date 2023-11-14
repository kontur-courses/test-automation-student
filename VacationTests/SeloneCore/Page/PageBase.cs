using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore.Controls;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
// Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
namespace SeloneCore.Page;

public abstract class PageBase : IPage
{
    protected PageBase(IContextBy contextBy, IControlFactory controlFactory)
    {
        ControlFactory = controlFactory;
        WrappedDriver = contextBy.SearchContext.WebDriver();
    }

    protected IControlFactory ControlFactory { get; }
    public IWebDriver WrappedDriver { get; }
}