using NUnit.Framework;
using VacationTests.Infrastructure;

namespace VacationTests
{
    [SetUpFixture]
    public class MyBrowserPoolTearDown
    {
        [OneTimeTearDown]
        //protected void OneTimeTearDown() => WebDriver.Close();
        protected static void OneTimeTearDown() => MyBrowserPool.Dispose();
    }
}