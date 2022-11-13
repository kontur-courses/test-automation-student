using System;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListTests : VacationTestBase
    {
        // Пользователя генерируем нового для каждого теста, чтобы тесты могли идти в параллель
        private string EmployeeId => TestContext.CurrentContext.Test.Name;

        // todo для курсанта: после создания рекорда (Задание 6) раскомментировать тесты и запустить
        /*
        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var employeeId = EmployeeId;
            var page = Navigation.OpenEmployeeVacationList(employeeId);
            // Проверка, что список пустой
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            // Создание 3 отпусков с дефолтными данными - для теста важно только количество отпусков
            var claims = new[]
            {
                Claim.CreateDefault() with {UserId = employeeId},
                Claim.CreateDefault() with {UserId = employeeId},
                Claim.CreateDefault() with {UserId = employeeId}
            }; // Создание абстракций
            ClaimStorage.Add(claims); // Запись их в базу

            // Обновление страницы
            page.Refresh();

            // Ожидание трёх строк в таблице
            page.ClaimList.Items.Count.Wait().EqualTo(3);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles()
        {
            var employeeId = EmployeeId;
            var page = Navigation.OpenEmployeeVacationList(employeeId);

            // Создание 3 отпусков с опредленными номерами, чтобы дальше их проверить
            // Остальные данные не важны для теста
            var claims = new[]
            {
                Claim.CreateDefault() with {Id = "1", UserId = employeeId},
                Claim.CreateDefault() with {Id = "2", UserId = employeeId},
                Claim.CreateDefault() with {Id = "3", UserId = employeeId}
            };
            ClaimStorage.Add(claims);
            page.Refresh();

            // Проверка данных по первому столбцу с учётом порядка
            // Если обновленеи списка будет не мгновенным,
            // то Wait() будет снова пробовать сделать поиск элементов, взятие их TitleLink.Text и сверку с ожиданием
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] {"Заявление 1", "Заявление 2", "Заявление 3"});

            // Проверка данных по первому столбцу без учёта порядка
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EquivalentTo(new[] {"Заявление 2", "Заявление 3", "Заявление 1"});
        }

        [Test]
        public void FirstClaim_ShouldDisplayRightDataInColumns()
        {
            var employeeId = EmployeeId;
            var page = Navigation.OpenEmployeeVacationList(employeeId);

            // Создание 2 отпусков
            var claims = new[]
            {
                Claim.CreateDefault() with
                {
                    Id = "1",
                    UserId = employeeId
                },
                Claim.CreateDefault() with
                {
                    Id = "2",
                    StartDate = new DateTime(2020, 8, 13),
                    EndDate = new DateTime(2020, 8, 20),
                    Status = ClaimStatus.Accepted,
                    UserId = employeeId
                }
            };

            ClaimStorage.Add(claims);
            page.Refresh();

            // Метод Single() находит элемент по признаку, проверяет, что нашелся только один элемент и возвращает его
            var claim = page.ClaimList.Items.Wait().Single(
                x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));

            // У полученного элемента проверяем разные поля
            // TODO: тут могут быть проблемы с датами и локалями.
            claim.PeriodLabel.Text.Wait().EqualTo("13.08.2020 - 20.08.2020");
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.Accepted.GetDescription());

            // Можем кликнуть по любому полю или просто по элементу
            claim.TitleLink.Click();
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightDataInColumns_IgnoringOrder()
        {
            var employeeId = EmployeeId;
            var page = Navigation.OpenEmployeeVacationList(employeeId);

            //  Создание 3 отпусков
            var claims = new[]
            {
                Claim.CreateDefault() with
                {
                    Id = "1",
                    StartDate = new DateTime(2020, 8, 1),
                    EndDate = new DateTime(2020, 8, 14),
                    Status = ClaimStatus.NonHandled,
                    UserId = employeeId
                },
                Claim.CreateDefault() with
                {
                    Id = "2",
                    StartDate = new DateTime(2020, 8, 13),
                    EndDate = new DateTime(2020, 8, 20),
                    Status = ClaimStatus.Accepted,
                    UserId = employeeId
                },
                Claim.CreateDefault() with
                {
                    Id = "3",
                    StartDate = new DateTime(2021, 5, 29),
                    EndDate = new DateTime(2021, 5, 30),
                    Status = ClaimStatus.Rejected,
                    UserId = employeeId
                }
            };
            ClaimStorage.Add(claims);
            page.Refresh();

            // Ожидаемые значения
            var expected = new[]
            {
                ("Заявление 3", "29.05.2021 - 30.05.2021", ClaimStatus.Rejected.GetDescription()),
                ("Заявление 2", "13.08.2020 - 20.08.2020", ClaimStatus.Accepted.GetDescription()),
                ("Заявление 1", "01.08.2020 - 14.08.2020", ClaimStatus.NonHandled.GetDescription())
            };

            // Проверка значений строк для выбранных столбцов (название заявления, период, статус)
            // Без учёта порядка элементов списка
            page.ClaimList.Items
                .Select(claim => Props.Create(claim.TitleLink.Text, claim.PeriodLabel.Text, claim.StatusLabel.Text))
                .Wait().EquivalentTo(expected);
            // Если посмотреть в этот метод расширения Wait, то он также обеспечивает,
            // что каждый раз при ожидании списка и перепроверке значений в его стобцах
            // все пропсы снова будут переопределяться
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightDataInColumns_InRightOrder()
        {
            var employeeId = EmployeeId;
            var page = Navigation.OpenEmployeeVacationList(employeeId);

            // Создание 3 отпусков
            var claims = new[]
            {
                Claim.CreateDefault() with
                {
                    Id = "1",
                    Status = ClaimStatus.NonHandled,
                    UserId = employeeId
                },
                Claim.CreateDefault() with
                {
                    Id = "2",
                    Status = ClaimStatus.Accepted,
                    UserId = employeeId
                },
                Claim.CreateDefault() with
                {
                    Id = "3",
                    Status = ClaimStatus.Rejected,
                    UserId = employeeId
                }
            };
            ClaimStorage.Add(claims);
            page.Refresh();

            // Ожидаемые значения
            var expectedColumns = new[]
            {
                ("Заявление 1", true),
                ("Заявление 2", true),
                ("Заявление 3", true)
            };
            // Проверка значений строк для выбранных столбцов (название заявления, видимость статуса)
            // с учётом порядка элементов списка,
            // в качестве Props можно брать любые пропсы, не только Text, но и, например, видимость или Checked
            page.ClaimList.Items.Select(x => Props.Create(x.TitleLink.Text, x.StatusLabel.Visible))
                .Wait()
                .EqualTo(expectedColumns);
        }

        [Test]
        public void ClaimsList_EachUserCanSeeOnlyHisClaims()
        {
            const string firstUserId = "employee1";
            const string secondUserId = "employee2";
            var page = Navigation.OpenEmployeeVacationList(firstUserId);

            var claims = new[]
            {
                Claim.CreateDefault() with {Id = "1", UserId = firstUserId},
                Claim.CreateDefault() with {Id = "2", UserId = secondUserId},
                Claim.CreateDefault() with {Id = "3", UserId = firstUserId}
            };
            ClaimStorage.Add(claims);
            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(2);

            // TODO: на UI показывается просто порядковый номер заявления, изменить на id
            // page.ClaimList.Items.Select(x => x.TitleLink.Text).Wait().EquivalentTo(new[] { "Заявление 1", "Заявление 3" });

            page = Navigation.OpenEmployeeVacationList(secondUserId);
            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(1);

            // TODO: на UI показывается просто порядковый номер заявления, изменить на id
            // page.ClaimList.Items.Select(x => x.TitleLink.Text).Wait().EquivalentTo(new[] { "Заявление 2" });
        }*/
    }
}