namespace EFCore
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private List<Person> _Residents;
        public List<Person> Residents { get { return _Residents; } }
        public City(string name)
        {
            Name = name;
            _Residents = new List<Person>();
        }
        public void AddResident(Person person)
        {
            if (person != null && !_Residents.Contains(person))
            {
                _Residents.Add(person);
            }
        }
    }
}