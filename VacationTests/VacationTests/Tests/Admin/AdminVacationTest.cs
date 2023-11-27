using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.RetryableAssertions;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;
using VacationTests.Utils;

namespace VacationTests.Tests.Admin
{
    public class AdminVacationTest : VacationTestBase
    {
        

        [Test]
        public void AdminClaimsList_ShouldDisplayRightData_InRightOrder()
        {
            
            var adminVacationListPage = Navigation.OpenAdminVacationListPage();
        }
        
    }
}