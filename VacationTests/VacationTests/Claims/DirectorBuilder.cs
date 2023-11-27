namespace VacationTests.Claims
{
    public class DirectorBuilder
    {
        // �������������� ��������� ���� �� ���������� �� ��������� ��� ������� �������� ������ Director
        private int id = 14;
        private string name = "������ �������� �������";
        private string position = "�������� ������������";

        public DirectorBuilder WithId(int newId)
        {
            id = newId;
            return this;
        }

        public DirectorBuilder WithName(string newName)
        {
            name = newName;
            return this;
        }

        public DirectorBuilder WithPosition(string newPosition)
        {
            position = newPosition;
            return this;
        }

        // �������� �����, ������� ���������� ��������� ������ Director
        public Director Build() => new Director(
            id,
            name,
            position
        );
    }
}