using static Model.school.Person;
namespace Model.school
{
    public class School
    {
        private List<Classroom> _Classrooms;
        public List<Classroom> Classrooms { get { return _Classrooms; } set { _Classrooms = value ?? new List<Classroom>(); } }
        private List<Student> _Students;
        public List<Student> Students { get { return _Students; } set { _Students = value ?? new List<Student>(); } }
        public School() 
        { 
            _Students = new List<Student>();
            _Classrooms = new List<Classroom>();
        }
        public void AddStudentToSchool(Student student) { _Students.Add(student); }
        public void AddClassroomToSchool(Classroom classroom) { _Classrooms.Add(classroom); }
        public int NumberOfStudents() { return _Students.Count(); }
        public int NumberOfMaleStudents() { return _Students.Where(a => a.Gender == AllowedGenders.m).Count(); }
        public int NumberOfFemaleStudents() { return _Students.Where(a => a.Gender == AllowedGenders.w).Count(); }
        public double AverageAgeOfStudents() { return _Students.Any() ? _Students.Average(a => a.Age) : 0; }
        public int NumberOfClassrooms() { return _Classrooms.Count(); }
        public List<Classroom> ClassroomsWithCynap() { return _Classrooms.Where(a => a.Cynap == true).ToList(); }
        public Dictionary<string, int> ClassroomsWithNumberOfStudents()
        {
            Dictionary<string, int> myDict = new Dictionary<string, int>( );
            foreach (var classroom in _Classrooms) 
            {
                myDict[classroom.ToString()] = classroom.Students.Count;
            }
            return myDict;
        }
        public double PercentOfFemaleStudentsInASchoolclass(Student.Schoolclasses b)
        {
            int numberFemaleStudents = _Students.Where(a => a.Gender == AllowedGenders.w && a.Schoolclass == b ).Count();
            int numberStudents = _Students.Where(a => a.Schoolclass == b).Count();
            return numberStudents == 0 ? 0 : (double)numberFemaleStudents / numberStudents * 100;
        }
        public bool IsClassroomBigEnough(Student.Schoolclasses s, Classroom c)
        {
            return _Students.Count(a => a.Schoolclass == s) <= c.Seats;
        }
    }
}
