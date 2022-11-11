using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using VacationTests.Infrastructure;

// О статических классах и свойствах https://ulearn.me/course/basicprogramming/Staticheskie_klassy_8adb5248-0361-4ca6-b2f9-851ead987603

[assembly: Parallelizable(ParallelScope.All)] // Настройка уровня параллелизации
[assembly: LevelOfParallelism(3)] // Количество потоков

namespace VacationTests
{
    [SetUpFixture]
    public class AssemblySetUpFixture
    {
        public static WebDriverPool WebDriverPool { get; private set; }

        // Метод запускается один раз перед всеми тестами, это обеспечивает атрибут SetUpFixture
        [OneTimeSetUp]
        public void SetUp()
        {
            // Инициализация пула
            var factory = new ChromeDriverFactory();
            var cleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            WebDriverPool = new WebDriverPool(factory, cleaner);
        }

        // Метод запускается один раз после всех тестов, это обеспечивает атрибут SetUpFixture
        [OneTimeTearDown]
        public void TearDown()
        {
            // Закрытие пула
            WebDriverPool.Clear();
        }
    }
}