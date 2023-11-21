using VacationTests.Claims;

namespace VacationTests.Data
{
    public static class Directors
    {
        public static Director Default => new DirectorBuilder().Build();
        public static Director SuperDirector => new DirectorBuilder()
            .WithId(24320).WithName("Кирпичников Алексей Николаевич")
            .WithPosition("Руководитель управления").Build();
        public static DirectorBuilder New => new DirectorBuilder();
    }
}