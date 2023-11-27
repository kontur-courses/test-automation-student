namespace VacationTests.Claims
{
    public record Director(
        // перечисляем какие свойства будут у класса Director
        int Id,
        string Name,
        string Position)
    {
        public static Director CreateDefault() =>
            new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
        public static Director CreateSuperDirector() =>
            new Director(24320, "Кирпичников Алексей Николаевич", "Руководитель управления");
    }
}