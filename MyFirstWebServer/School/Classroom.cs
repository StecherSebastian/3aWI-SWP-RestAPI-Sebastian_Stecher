namespace ProgrammUE_02
{
    public class Classroom : Room
    {
        private List<Student> _Students;
        public List<Student> Students { get { return _Students; } }
        private int _Seats;
        public int Seats { get { return _Seats; } }
        private bool _Cynap;
        public bool Cynap { get { return _Cynap; } }
        public Classroom(int size, int seats, bool cynap) : base(size)
        {
            _Seats = size;
            _Cynap = cynap;
            _Students = new List<Student>();
        }
        public void AddStudentToClassroom(Student student) { _Students.Add(student); }
    }
}
