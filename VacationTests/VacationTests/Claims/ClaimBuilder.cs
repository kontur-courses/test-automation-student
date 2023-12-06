using System;

namespace VacationTests.Claims
{
    // Builder – это тоже класс
    // Название должно быть обязательно <имя класса, для которого создаем Builder>Builder
    public class ClaimBuilder
    {
        // Инициализируем приватные поля со значениями по умолчанию для каждого свойства класса Claim
        private const string DefaultUserId = "1";
        private string id = new Random().Next(101).ToString();
        private ClaimType type = ClaimType.Paid;
        private ClaimStatus status = ClaimStatus.NonHandled;
        private string userId = DefaultUserId;
        private int? childAgeInMonths;

        // Для каждого поля создаем метод With<название свойства>, возвращающий экземпляр этого ClaimBuilder
        // Метод принимает значение и записывает в соответствующее приватное поле
        // С помощью таких методов можно будет задать необходимые поляr
        public ClaimBuilder WithId(string newId)
        {
            id = newId;
            return this;
        }

        public ClaimBuilder WithType(ClaimType newClaimType)
        {
            type = newClaimType;
            return this;
        }

        public ClaimBuilder WithStatus(ClaimStatus newStatus)
        {
            status = newStatus;
            return this;
        }

        public ClaimBuilder WithUserId(string newUserId)
        {
            userId = newUserId;
            return this;
        }

        public ClaimBuilder WithUserId(int newUserId)
        {
            userId = newUserId.ToString();
            return this;
        }

        public ClaimBuilder WithChildAgeInMonths(int newChildAgeInMonths)
        {
            childAgeInMonths = newChildAgeInMonths;
            return this;
        }

        // Основной метод, который возвращает экземпляр класса Claim
        public Claim Build() => new Claim(
            id,
            type,
            status,
            new Director(14, "Бублик Владимир Кузьмич", "Директор департамента"),
            DateTime.Now.Date.AddDays(7),
            DateTime.Now.Date.AddDays(12),
            childAgeInMonths,
            userId,
            false
        );
    }
}