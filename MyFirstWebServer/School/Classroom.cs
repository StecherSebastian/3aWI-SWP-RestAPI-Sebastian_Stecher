using System.Text.Json.Serialization;
namespace SchoolNamespace
{
    public class Classroom : Room
    {
        private List<Student> _Students;
        public List<Student> Students { get { return _Students; } set { _Students = value ?? new List<Student>(); } }
        private int _Seats;
        public int Seats { get { return _Seats; } }
        private bool _Cynap;
        public bool Cynap { get { return _Cynap; } }
        public Classroom() : base() { _Students = new List<Student>(); }
        [JsonConstructor]
        public Classroom(int size, int seats, bool cynap) : base(size)
        {
            _Seats = seats;
            _Cynap = cynap;
            _Students = new List<Student>();
        }
        public void AddStudentToClassroom(Student student) { _Students.Add(student); }
        public override string ToString() => $"Classroom_Size:{Size}_Seats:{Seats}_Cynap:{Cynap}";
    }
}
