using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VacationTests.Claims;
using VacationTests.Data;

public record Claim
(
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
    public string GetTitle()
    {
        return $"Заявление {Id}";
    }

    public string GetUserFIO()
    {
        return $"Пользователь {UserId}";
    }

    public static Claim CreateDefault()
    {
        var random = new Random();
        var randomClaimId = random.Next(1, 101).ToString();
        var startDate = DateTime.Today.Date.AddDays(7);
        var endDate = DateTime.Today.Date.AddDays(12);

        return new Claim(
            randomClaimId,
            ClaimType.Paid,
            ClaimStatus.NonHandled,
            Directors.Default, 
            startDate,
            endDate,
            null,
            "1",
            false
        );
    }
        
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

namespace VacationTests.Claims
{
}