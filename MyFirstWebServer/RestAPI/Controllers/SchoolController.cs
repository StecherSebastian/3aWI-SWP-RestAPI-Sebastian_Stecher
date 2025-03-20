using Microsoft.AspNetCore.Mvc;
using SchoolNamespace;
using static SchoolNamespace.Person;
using static SchoolNamespace.Student;

[ApiController]
[Route("api/[controller]")]
public class SchoolController : ControllerBase
{
    [HttpPut("newSchool")]
    public IActionResult NewSchool()
    {
        try
        {
            School school = new School();
            int schoolID = DataStore.SchoolIDCounter++;
            DataStore.Schools[schoolID] = school;
            return Ok(new { SchoolID = schoolID, School = school, Message = "New school created successfully." });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("newClassroom")]
    public IActionResult NewClassroom([FromBody] ClassroomRequestBody request)
    {
        try
        {
            Classroom classroom = new Classroom(request.Size, request.Seats, request.Cynap);
            int classroomID = DataStore.ClassroomIDCounter++;
            DataStore.Classrooms[classroomID] = classroom;
            return Ok(new { ClassroomID = classroomID, Classroom = classroom, Message = "New classroom created successfully." });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("newStudent")]
    public IActionResult NewStudent([FromBody] StudentRequestBody request)
    {
        try
        {
            Student student = new Student(request.Schoolclass, request.Gender, request.Birthdate);
            int studentID = DataStore.StudentIDCounter++;
            DataStore.Students[studentID] = student;
            return Ok(new { StudentID = studentID, Student = student, Message = "New student created successfully." });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("addStudentToSchool")]
    public IActionResult AddStudentToSchool([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (request.StudentID == null) { return BadRequest(Messages.StudentRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            if (!DataStore.Students.TryGetValue(request.StudentID.Value, out Student student)) { return NotFound(Messages.StudentNotFound); }
            school.AddStudentToSchool(student);
            return Ok(new {School = school, Message = "Student added successfully." });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("addClassroomToSchool")]
    public IActionResult AddClassroomToSchool([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (request.ClassroomID == null) { return BadRequest(Messages.ClassroomRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            if (!DataStore.Classrooms.TryGetValue(request.ClassroomID.Value, out Classroom classroom)) { return NotFound(Messages.ClassroomNotFound); }
            school.AddClassroomToSchool(classroom);
            return Ok(new { School = school, Message = "Classroom added successfully." });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("addStudentToClassroom")]
    public IActionResult AddStudentToClassroom([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.ClassroomID == null) { return BadRequest(Messages.ClassroomRequired); }
            if (request.StudentID == null) { return BadRequest(Messages.StudentRequired); }
            if (!DataStore.Classrooms.TryGetValue(request.ClassroomID.Value, out Classroom classroom)) { return NotFound(Messages.ClassroomNotFound); }
            if (!DataStore.Students.TryGetValue(request.StudentID.Value, out Student student)) { classroom.AddStudentToClassroom(student); }
            return Ok(new { Classroom = classroom, Message = "Student added successfully." });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("NumberOfStudents")]
    public IActionResult NumberOfStudents([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new {School = school, Message = $"Number of Students in School: {school.NumberOfStudents()}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("NumberOfMaleStudents")]
    public IActionResult NumberOfMaleStudents([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new {School = school, Message = $"Number of male Students in School: {school.NumberOfMaleStudents()}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("NumberOfFemaleStudents")]
    public IActionResult NumberOfFemaleStudents([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new {School = school, Message = $"Number of female Students in School: {school.NumberOfFemaleStudents()}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("AverageAgeOfStudents")]
    public IActionResult AverageAgeOfStudents([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new {School = school, Message = $"Average age of Students in School: {school.AverageAgeOfStudents()}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("NumberOfClassrooms")]
    public IActionResult NumberOfClassrooms([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new {School = school, Message = $"Number of Classrooms in School: {school.NumberOfClassrooms()}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("ClassroomsWithSynap")]
    public IActionResult ClassroomsWithCynap([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new { School = school, Message = $"Classrooms with Cynap: {school.ClassroomsWithCynap()}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("ClassroomsWithNumberOfStudents")]
    public IActionResult ClassroomsWithNumberOfStudents([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new { School = school, Message = $"Classrooms with number of Students: {school.ClassroomsWithNumberOfStudents()}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("PercentOfFemaleStudentsInASchoolclass")]
    public IActionResult PercentOfFemaleStudentsInASchoolclass([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (request.Schoolclass == null) { return BadRequest(Messages.SchoolclassRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            return Ok(new { School = school, Schoolclass = request.Schoolclass, Message = $"Percentage of female Students in a Schoolclass: {school.PercentOfFemaleStudentsInASchoolclass(request.Schoolclass.Value)}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("IsClassroomBigEnough")]
    public IActionResult IsClassroomBigEnough([FromBody] ObjectsRequestBody request)
    {
        try
        {
            if (request.SchoolID == null) { return BadRequest(Messages.SchoolRequired); }
            if (request.ClassroomID == null) { return BadRequest(Messages.ClassroomRequired); }
            if (request.Schoolclass == null) { return BadRequest(Messages.SchoolclassRequired); }
            if (!DataStore.Schools.TryGetValue(request.SchoolID.Value, out School school)) { return NotFound(Messages.SchoolNotFound); }
            if (!DataStore.Classrooms.TryGetValue(request.ClassroomID.Value, out Classroom classroom)) { return NotFound(Messages.ClassroomNotFound); }
            return Ok(new { School = school, Classroom = classroom, Schoolclass = request.Schoolclass, Message = $"Is the Classroom big enough for the Schoolclass: {school.IsClassroomBigEnough(request.Schoolclass.Value, classroom)}" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("listSchools")]
    public IActionResult ListSchools()
    {
        try
        {
            return Ok(DataStore.Schools.ToDictionary());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("listStudents")]
    public IActionResult ListStudents()
    {
        try
        {
            return Ok(DataStore.Students.ToDictionary());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("listClassrooms")]
    public IActionResult ListClassrooms()
    {
        try
        {
            return Ok(DataStore.Classrooms.ToDictionary());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
public class ClassroomRequestBody
{
    public int Size { get; set; }
    public int Seats { get; set; }
    public bool Cynap { get; set; }
}
public class StudentRequestBody
{
    public Schoolclasses Schoolclass { get; set; }
    public AllowedGenders Gender { get; set; }
    public DateTime Birthdate { get; set; }
}
public class ObjectsRequestBody
{
    public int? SchoolID { get; set; }
    public int? ClassroomID { get; set; }
    public int? StudentID { get; set; }
    public Schoolclasses? Schoolclass { get; set; }
}
public static class DataStore
{
    internal static int SchoolIDCounter = 0;
    internal static int ClassroomIDCounter = 0;
    internal static int StudentIDCounter = 0;
    public static Dictionary<int, School> Schools = new();
    public static Dictionary<int, Student> Students = new();
    public static Dictionary<int, Classroom> Classrooms = new();
}

public static class Messages
{
    public static string SchoolNotFound = "School not found.";
    public static string ClassroomNotFound = "Classroom not found";
    public static string StudentNotFound = "Student not found";
    public static string SchoolRequired = "School required";
    public static string ClassroomRequired = "Classroom required";
    public static string StudentRequired = "Student required";
    public static string SchoolclassRequired = "Schoolclass required";
}