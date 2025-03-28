namespace ConsoleAppWithEFCore
{
    public class Person : Human
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Person(string firstName, string lastName, DateTime birthdate, AllowedGenders gender) : base(birthdate, gender)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
