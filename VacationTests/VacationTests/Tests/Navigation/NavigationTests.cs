﻿using FluentAssertions;
using NUnit.Framework;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : VacationTestBase
    {
        [Test]
        public void LoginPage_GoToAdminPageTest()
        {
            var enterPage = Navigation.OpenLoginPage();
            enterPage.WaitLoaded();
            var adminPage = enterPage.LoginAsAdmin();
            adminPage.WaitLoaded();
            adminPage.IsAdminPage.Should().BeTrue();
        }
    }
}