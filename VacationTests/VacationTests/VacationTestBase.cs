using NUnit.Framework;
using OpenQA.Selenium;
using SeloneCore.Controls;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.PageObjects.Navigations;

namespace VacationTests;

public abstract class VacationTestBase
{
    private IWebDriver WebDriver = new ChromeDriverFactory().Create();
    protected ClaimStorage ClaimStorage => new(LocalStorage);
    protected LocalStorage LocalStorage => new(WebDriver);
    private IControlFactory ControlFactory => new ControlFactory(LocalStorage, ClaimStorage);
    protected Navigation Navigation => new(WebDriver, ControlFactory);
    private Screenshoter Screenshoter => new(WebDriver); 
    
    [OneTimeTearDown]
    protected void OneTimeTearDown() => WebDriver.Dispose();
        
    [TearDown]
    public void TearDown() => Screenshoter.SaveTestFailureScreenshot();
}