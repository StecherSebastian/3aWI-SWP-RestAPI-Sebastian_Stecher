using System.ComponentModel;

namespace ProgrammUE_02
{
    public class Person
    {
        private DateTime _Birthdate;
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

        public Person(DateTime birthdate, AllowedGenders gender) 
        {
            _Birthdate = birthdate;
            _Gender = gender;
        }
    }
}
