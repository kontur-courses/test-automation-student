namespace VacationTests.Claims
{
    public class Director
    {
        public Director(int id, string name, string position)
        {
            Id = id;
            Name = name;
            Position = position;
        }

        public int Id { get; }
        public string Name { get; }
        public string Position { get; }
    }
}