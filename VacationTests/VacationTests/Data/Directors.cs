using VacationTests.Claims;

namespace VacationTests.Data
{
    //public static class Directors
    //{
    //    public static Director Default => new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
    //    public static Director SuperDirector => new Director(24320, "Кирпичников Алексей Николаевич", "Руководитель управления");
    //    public static DirectorBuilder New => new DirectorBuilder();
    //}
    public static class Directors
    {
        public static Director Default => new DirectorBuilder().WithId(14).WithName("Бублик Владимир Кузьмич").WithPosition("Директор департамента").Build();
        public static Director SuperDirector => new DirectorBuilder().WithId(24320).WithName("Кирпичников Алексей Николаевич").WithPosition("Руководитель управления").Build();
        public static DirectorBuilder New => new DirectorBuilder();
    }
}