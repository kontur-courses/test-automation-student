using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;
using VacationTests.Infrastructure;
using VacationTests.PageObjects.Controls;

namespace VacationTests.PageObjects.Pages;

public class InfoSidePage : PageBase
{
    // Lazy<T> – ещё один вариант работы с элементами, которых надо искать лениво
    // Свойство Value вернёт выполненный код

    private readonly Lazy<Button> agreeButton;
    private readonly Lazy<Label> bodyLabel;
    private readonly Lazy<Button> closeButton;

    private readonly Lazy<Button> crossButton;
    private readonly Lazy<Label> headerLabel;
    private readonly Lazy<Button> notAgreeButton;

    //TODO pe: Тут и в ClaimModal стоило бы придумать как перейти на InjectControls, чтобы не писать ручное создание
    public InfoSidePage(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
        headerLabel = CreateLazyControlByTid<Label>("SidePageHeader");
        crossButton = CreateLazyControlByTid<Button>("SidePage__close");
        bodyLabel = CreateLazyControlByTid<Label>("SidePageBody");
        agreeButton = CreateLazyControlByTid<Button>("AgreeButton");
        notAgreeButton = CreateLazyControlByTid<Button>("NotAgreeButton");
        closeButton = CreateLazyControlByTid<Button>("CloseButton");
    }

    [ByTid("InfoSidePage")] public Portal InfoSidePageModel { get; private set; }

    public Label HeaderLabel => headerLabel.Value;
    public Button CrossButton => crossButton.Value;
    public Label BodyLabel => bodyLabel.Value;
    public Button AgreeButton => agreeButton.Value;
    public Button NotAgreeButton => notAgreeButton.Value;
    public Button CloseButton => closeButton.Value;

    private Lazy<TControl> CreateLazyControlByTid<TControl>(string tid) where TControl : ControlBase
    {
        return new(() =>
            PageObjectFactory.CreateControl<TControl>(
                GetModalContext().Search(x => SelectorExtensions.WithTid((ByDummy) x, tid)))
        );
    }

    private IWebElement GetModalContext()
    {
        return InfoSidePageModel.GetPortalElement();
    }
}