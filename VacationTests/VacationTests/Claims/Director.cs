namespace VacationTests.Claims
{
    public record Director(
        // ����������� ����� �������� ����� � ������ Director
        int Id,
        string Name,
        string Position)
    {
        public static Director CreateDefault() =>
            new Director(14, "������ �������� �������", "�������� ������������");
        public static Director CreateSuperDirector() =>
            new Director(24320, "����������� ������� ����������", "������������ ����������");
    }
}