using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using VacationTests.DependencyInjection;
using VacationTests.DependencyInjection.MethodInject;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests: IFixtureWithServiceProviderFramework
    {
        public void ConfigureServices(IServiceCollection collection) => collection.AddWeb();
        
        [Test]
        public void LoginAsAdmin_CheckAdminVacationListPageIsOpen([Inject] PageNavigation.Navigation navigation)
        {
            var enterPage = navigation.OpenLoginPage();
            enterPage.WaitLoaded();
            enterPage.TitleLabel.Text.Should().Be("Вход в сервис");

            var page = enterPage.LoginAsAdmin();
            page.TitleLabel.Text.Should().Be("Список отпусков");
        }
    }
}