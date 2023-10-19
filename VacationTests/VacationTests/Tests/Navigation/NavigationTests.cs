using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : VacationTestBase
    {
        [Test]
        public void LoginPage_GoToAdminPageTest()
        {
            var enterPage = Navigation.OpenLoginPage();
            var adminPage = enterPage.LoginAsAdmin();
            adminPage.IsAdminPage.Should().BeTrue();
            
            // var collection = new []
            // {
            //     new { Id = 1, Name = "John", Attributes = new string[] { } },
            //     new { Id = 2, Name = "Jane", Attributes = new string[] {"attr"} }
            // };
            //
            // collection.Should().SatisfyRespectively(
            //     first =>
            //     {
            //         first.Id.Should().Be(2);
            //         first.Name.Should().StartWith("r");
            //         first.Attributes.Should().NotBeNull();
            //     },
            //     second =>
            //     {
            //         second.Id.Should().Be(3);
            //         second.Name.Should().EndWith("t");
            //         second.Attributes.Should().NotBeEmpty();
            //     });
            // using (new AssertionScope())
            // {
            //     adminPage.IsAdminPage.Should().BeFalse();
            //     adminPage.ClaimsTab.Should().BeNull();
            // }
        }
    }
}