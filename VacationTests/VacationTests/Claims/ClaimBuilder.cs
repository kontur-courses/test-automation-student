using System;
using VacationTests.Data;

namespace VacationTests.Claims
{
    // Builder – это тоже класс
    // Название должно быть обязательно <имя класса, для которого создаем Builder>Builder
    public class ClaimBuilder
    {
        // Инициализируем приватные поля со значениями по умолчанию для каждого свойства класса Claim
        private const string DefaultUserId = "1";
        private int? _childAgeInMonths;
        private string _id = new Random().Next(101).ToString();
        private ClaimStatus _status = ClaimStatus.NonHandled;
        private ClaimType _type = ClaimType.Paid;
        private string _userId = DefaultUserId;
        private Director _director = Directors.Default;
        private DateTime _startDate = DateTime.Today.AddDays(7);
        private DateTime _endDate = DateTime.Today.AddDays(12);
        private bool _paidNow = false;

        // Для каждого поля создаем метод With<название свойства>, возвращающий экземпляр этого DirectorBuilder
        // Метод принимает значение и записывает в соответствующее приватное поле
        // С помощью таких методов можно будет задать необходимые поляr
        public ClaimBuilder WithId(string newId)
        {
            _id = newId;
            return this;
        }

        public ClaimBuilder WithType(ClaimType newClaimType)
        {
            _type = newClaimType;
            return this;
        }

        public ClaimBuilder WithStatus(ClaimStatus newStatus)
        {
            _status = newStatus;
            return this;
        }

        public ClaimBuilder WithUserId(string newUserId)
        {
            _userId = newUserId;
            return this;
        }

        public ClaimBuilder WithUserId(int newUserId)
        {
            _userId = newUserId.ToString();
            return this;
        }

        public ClaimBuilder WithChildAgeInMonths(int newChildAgeInMonths)
        {
            _childAgeInMonths = newChildAgeInMonths;
            return this;
        }
        
        public ClaimBuilder WithDirector(Director director)
        {
            _director = director;
            return this;
        }
        
        public ClaimBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate.Date;
            return this;
        }
        
        public ClaimBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate.Date;
            return this;
        }

        public ClaimBuilder WithPeriod(DateTime startDate, DateTime endDate)
        {
            if(endDate < startDate) 
                throw new Exception("Дата начала отпуска должна быть раньше даты конца отпуска");
            if ((endDate - startDate).Days < 3) 
                throw new Exception("Минимальный период отпуска должен быть 3 дня");
            _startDate = startDate.Date;
            _endDate = endDate.Date;
            return this;
        }

        public ClaimBuilder WithPaidNow(bool paidNow)
        {
            _paidNow = paidNow;
            return this;
        }

        // Основной метод, который возвращает экземпляр класса Claim
        public Claim Build()
        {
            return new(
                _id,
                _type,
                _status,
                _director,
                _startDate,
                _endDate,
                _childAgeInMonths,
                _userId,
                _paidNow
            );
        }
    }
}