using System;
using VacationTests.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VacationTests.Claims
{
    public record Claim(
    // перечисляем какие свойства будут у класса Claim
    string Id,
    [property: JsonConverter(typeof(StringEnumConverter))]
    ClaimType Type,
    ClaimStatus Status,
    Director Director,
    DateTime StartDate,
    DateTime EndDate,
    int? ChildAgeInMonths,
    string UserId,
    bool PaidNow
    )
    {
        // добавляем статический метод для создания экземпляра класса со значениями по умолчанию
        public static Claim CreateDefault()
        {
            var random = new Random();
            var randomClaimId = random.Next(1, 101).ToString();

            return new Claim(
                randomClaimId,
                ClaimType.Paid,
                ClaimStatus.NonHandled,
                Directors.Default,
                DateTime.Now.Date.AddDays(7),
                DateTime.Now.Date.AddDays(12),
                null,
                "1",
                false
            );
        }

        // можно также добавить второй метод для создания типичного заявления по уходу за ребёнком
        public static Claim CreateChildType()
        {
            var random = new Random();
            var childAgeInMonths = random.Next(1, 101);
            return CreateDefault() with
            {
                Type = ClaimType.Child,
                ChildAgeInMonths = childAgeInMonths
            };
        }
    }
}