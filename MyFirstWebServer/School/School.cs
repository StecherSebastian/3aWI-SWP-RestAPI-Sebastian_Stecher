using static ProgrammUE_02.Person;
namespace ProgrammUE_02
{
    public class School
    {
        private List<Classroom> _Classrooms;
        public List<Classroom> Classrooms { get { return _Classrooms; } }
        private List<Student> _Students;
        public List<Student> Students { get { return _Students; } }
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
        public double AverageAgeOfStudents() { return _Students.Average(a => a.Age); }
        public int NumberOfClassrooms() { return _Classrooms.Count(); }
        public List<Classroom> ClassroomsWithSynap() { return _Classrooms.Where(a => a.Cynap == true).ToList(); }
        public Dictionary<Classroom, int> ClassroomsWithNumberOfStudents()
        {
            Dictionary<Classroom, int> myDict = new Dictionary<Classroom, int>( );
            foreach (var classroom in _Classrooms) 
            {
                myDict[classroom] = classroom.Students.Count();
            }
            return myDict;
        }
        public double PercentOfFemaleStudentsInASchoolclass(Student.Schoolclasses b)
        {
            int numberFemaleStudents = _Students.Where(a => a.Gender == AllowedGenders.w && a.Schoolclass == b ).Count();
            int numberStudents = _Students.Where(a => a.Schoolclass == b).Count();
            return numberFemaleStudents / numberStudents * 100;
        }
        public bool IsClassroomBigEnough(Student.Schoolclasses s, Classroom c)
        {
            int numberStudents = _Students.Where(a => a.Schoolclass == s).Count();
            if (numberStudents > c.Seats)
            {
                return false;
            }
            return true;
        }
    }
}
