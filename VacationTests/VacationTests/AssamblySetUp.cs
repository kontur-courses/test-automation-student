using NUnit.Framework;

// Классы с тестами запускаются параллельно.
// Тесты внутри одного класса проходят последовательно.
[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace VacationTests;

[SetUpFixture]
public class AssemblySetUp
{
    [OneTimeSetUp]
    public async Task OneTimeSetUpAsync()
    {
        await TestContext.Out.WriteLineAsync(
            $"{nameof(AssemblySetUp)} -> {nameof(OneTimeSetUpAsync)} Запустили тесты в {DateTime.Now}"
        );
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDownAsync()
    {
        await TestContext.Out.WriteLineAsync(
            $"{nameof(AssemblySetUp)} -> {nameof(OneTimeTearDownAsync)} Запустили тесты в {DateTime.Now}"
        );
    }
}