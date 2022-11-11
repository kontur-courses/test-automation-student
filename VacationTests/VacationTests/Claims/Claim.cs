using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VacationTests.Claims
{
    // Об enum https://ulearn.me/course/basicprogramming/Konstanty_i_enum_y_f1740706-b8e2-4bd4-ab87-3cc710a52449

    public class Claim
    {
        // Метод инициализации
        public Claim(string id, ClaimType type, ClaimStatus status, Director director, DateTime startDate,
            DateTime endDate, int? childAgeInMonths, string userId, bool paidNow)
        {
            Id = id;
            Type = type;
            Status = status;
            Director = director;
            StartDate = startDate;
            EndDate = endDate;
            ChildAgeInMonths = childAgeInMonths;
            UserId = userId;
            PaidNow = paidNow;
        }
    
        // Свойства класса
        public string Id { get; }
    
        [property: JsonConverter(typeof(StringEnumConverter))]
        public ClaimType Type { get; }
    
        public ClaimStatus Status { get; }
    
        public Director Director { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int? ChildAgeInMonths { get; }
        public string UserId { get; }
        public bool PaidNow { get; }
    }
}