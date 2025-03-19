namespace ProgrammUE_02
{
    public class Student : Person
    {
        public enum Schoolclasses 
        {
            Class3aWI = 0,
            Class3bWI = 1
        }
        private Schoolclasses _Schoolclass;
        public Schoolclasses Schoolclass { get { return _Schoolclass; } }
        public Student(Schoolclasses schoolclass, AllowedGenders gender, DateTime birthdate) : base(birthdate, gender)
        {
            _Schoolclass = schoolclass;
        }
    }
}
