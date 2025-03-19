using SchoolNamespace;
using static SchoolNamespace.Person;
using static SchoolNamespace.Student;

namespace SchoolUnitTests
{
    public class Tests
    {
        private SchoolNamespace.School _School;
        private Student _Student;
        private Student _Student2;
        private Student _Student3;
        private Classroom _Classroom;
        private Classroom _Classroom2;
        [SetUp]
        public void Setup()
        {
            _School = new SchoolNamespace.School();
            _Student = new Student(0, 0, new DateTime(2008, 05, 10));
            _Student2 = new Student((Schoolclasses)1, (AllowedGenders)1, new DateTime(2007, 11, 21));
            _Student3 = new Student((Schoolclasses)1, (AllowedGenders)1, new DateTime(2008, 10, 19));
            _Classroom = new Classroom(35, 20, true);
            _Classroom2 = new Classroom(35, 25, false);
        }

        [Test]
        public void AddStudentToSchool_ReturnsObjectStudent()
        {
            _School.AddStudentToSchool(_Student);
            Assert.That(_School.Students[0], Is.EqualTo(_Student));
        }
        [Test]
        public void AddClassroomToSchool_ReturnsObjectClassroom()
        {
            _School.AddClassroomToSchool(_Classroom);
            Assert.That(_School.Classrooms[0], Is.EqualTo(_Classroom));
        }
        [Test]
        public void AddStudentToClassroom_ReturnsObjectStudent()
        {
            _Classroom.AddStudentToClassroom(_Student);
            Assert.That(_Classroom.Students[0], Is.EqualTo(_Student));
        }
        [Test]
        public void NumberOfStudents_AddOneStudent_ReturnsIntWithValueOne()
        {
            _School.AddStudentToSchool(_Student);
            Assert.That(_School.NumberOfStudents(), Is.EqualTo(1));
        }
        [Test]
        public void NumberOfMaleStudents_AddTwoStudents_ReturnsIntWithValueOne()
        {
            _School.AddStudentToSchool(_Student);
            _School.AddStudentToSchool(_Student2);
            Assert.That(_School.NumberOfMaleStudents, Is.EqualTo(1));
        }
        [Test]
        public void NumberOfFemaleStudents_AddThreeStudents_ReturnsIntWithValueOne()
        {
            _School.AddStudentToSchool(_Student);
            _School.AddStudentToSchool(_Student2);
            _School.AddStudentToSchool(_Student3);
            Assert.That(_School.NumberOfFemaleStudents, Is.EqualTo(2));
        }
        [Test]
        public void AverageAgeOfStudents_AddTwoStudents_ReturnsDouble()
        {
            var student1Birthdate = new DateTime(2007, 11, 21);
            var student2Birthdate = new DateTime(2008, 10, 19);
            _School.AddStudentToSchool(new Student(0, (AllowedGenders)1, student1Birthdate));
            _School.AddStudentToSchool(new Student(0, (AllowedGenders)1, student2Birthdate));
            int student1Age = DateTime.Now.Year - student1Birthdate.Year;
            int student2Age = DateTime.Now.Year - student2Birthdate.Year;
            double averageAge = (student1Age + student2Age) / 2.0;
            Assert.That(_School.AverageAgeOfStudents(), Is.EqualTo(averageAge));
        }
        [Test]
        public void NumberOfClassrooms_AddOneClassroom_ReturnsIntWithValueOne()
        {
            _School.AddClassroomToSchool(_Classroom);
            Assert.That(_School.NumberOfClassrooms, Is.EqualTo(1));
        }
        [Test]
        public void ClassroomsWithCynap_AddTwoClassrooms_ReturnObjectClassroom()
        {
            _School.AddClassroomToSchool(_Classroom);
            _School.AddClassroomToSchool(_Classroom2);
            Assert.That(_School.ClassroomsWithCynap()[0], Is.EqualTo(_Classroom));
        }
        [Test]
        public void ClassroomsWithNumberOfStudents_AddThreeStudentAndOneSchool_ReturnsDict()
        {
            Dictionary<Classroom, int> myDict = new Dictionary<Classroom, int> { { _Classroom, 3 } };
            _Classroom.AddStudentToClassroom(_Student);
            _Classroom.AddStudentToClassroom(_Student2);
            _Classroom.AddStudentToClassroom(_Student3);
            _School.AddClassroomToSchool(_Classroom);
            Assert.That(_School.ClassroomsWithNumberOfStudents(), Is.EqualTo(myDict));
        }
        [Test]
        public void PercentOfFemalStudentsInAClass_AddTwoFemaleStudents_ReturnsDouble100()
        {
            _School.AddStudentToSchool(_Student2);
            _School.AddStudentToSchool(_Student3);
            Assert.That(_School.PercentOfFemaleStudentsInASchoolclass((Student.Schoolclasses)1), Is.EqualTo(100.0));
        }
        [Test]
        public void PercentOfFemalStudentsInAClass_AddZeroStudents_ReturnsDouble0()
        {
            Assert.That(_School.PercentOfFemaleStudentsInASchoolclass((Student.Schoolclasses)1), Is.EqualTo(0.0));
        }
        [Test]
        public void IsClassroomBigEnough()
        {
            Assert.That(_School.IsClassroomBigEnough((Student.Schoolclasses)1, _Classroom), Is.True);
        }
    }
}