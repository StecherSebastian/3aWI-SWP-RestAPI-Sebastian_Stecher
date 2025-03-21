using System.Text.Json.Serialization;
namespace Model.school
{
    public class Student : Person
    {
        public enum Schoolclasses 
        {
            Class3aWI = 0,
            Class3bWI = 1
        }
        private Schoolclasses _Schoolclass;
        public Schoolclasses Schoolclass { get { return _Schoolclass; } private set { _Schoolclass = value; } }
        public Student() : base() { }
        [JsonConstructor]
        public Student(Schoolclasses schoolclass, AllowedGenders gender, DateTime birthdate) : base(birthdate, gender)
        {
            _Schoolclass = schoolclass;
        }
    }
}
