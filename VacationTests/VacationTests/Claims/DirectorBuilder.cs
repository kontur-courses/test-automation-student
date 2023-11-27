namespace VacationTests.Claims
{
    public class DirectorBuilder
    {
        private int _id = 14;
        private string _name = "Бублик Владимир Кузьмич";
        private string _position = "Директор департамента";

        public DirectorBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public DirectorBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public DirectorBuilder WithPosition(string position)
        {
            _position = position;
            return this;
        }

        public Director Build() => new(_id, _name, _position);
    }
}