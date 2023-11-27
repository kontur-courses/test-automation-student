namespace VacationTests.Claims
{
    public class DirectorBuilder
    {
        // Инициализируем приватные поля со значениями по умолчанию для каждого свойства класса Director
        private int id = 14;
        private string name = "Бублик Владимир Кузьмич";
        private string position = "Директор департамента";

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

        // Основной метод, который возвращает экземпляр класса Director
        public Director Build() => new Director(
            id,
            name,
            position
        );
    }
}