using System.Text.Json.Serialization;
namespace SchoolNamespace
{
    public class Person
    {
        private DateTime _Birthdate;
        public DateTime Birthdate { get { return _Birthdate; } private set { _Birthdate = value; } }
        public int Age { get { return DateTime.Now.Year - _Birthdate.Year; } }
        public enum AllowedGenders
        {
            m = 0,
            w = 1,
            d = 2
        }
        private AllowedGenders _Gender;
        public AllowedGenders Gender 
        { 
            get { return _Gender; } 
            set { _Gender = value; } 
        }
        public Person() {}
        [JsonConstructor]
        public Person(DateTime birthdate, AllowedGenders gender) 
        {
            _Birthdate = birthdate;
            _Gender = gender;
        }
    }
}
