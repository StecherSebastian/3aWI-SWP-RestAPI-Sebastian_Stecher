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
            return Ok(new School());
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
            return Ok(new Classroom(request.size, request.seats, request.cynap));
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
            return Ok(new Student(request.schoolclass, request.gender, request.birthdate));
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
            request.school.AddStudentToSchool(request.student);
            return Ok();
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
            request.school.AddClassroomToSchool(request.classroom);
            return Ok();
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
            request.classroom.AddStudentToClassroom(request.student);
            return Ok();
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
            return Ok(request.school.NumberOfStudents());
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
            return Ok(request.school.NumberOfMaleStudents());
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
            return Ok(request.school.NumberOfFemaleStudents());
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
            return Ok(request.school.AverageAgeOfStudents());
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
            return Ok(request.school.NumberOfClassrooms());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("ClassroomsWithSynap")]
    public IActionResult ClassroomsWithSynap([FromBody] ObjectsRequestBody request)
    {
        try
        {
            return Ok(request.school.ClassroomsWithCynap());
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
            return Ok(request.school.ClassroomsWithNumberOfStudents());
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
            return Ok(request.school.PercentOfFemaleStudentsInASchoolclass(request.schoolclass));
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
            return Ok(request.school.IsClassroomBigEnough(request.schoolclass, request.classroom));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
public class ClassroomRequestBody
{
    public int size { get; set; }
    public int seats { get; set; }
    public bool cynap { get; set; }
}
public class StudentRequestBody
{
    public Schoolclasses schoolclass { get; set; }
    public AllowedGenders gender { get; set; }
    public DateTime birthdate { get; set; }
}
public class ObjectsRequestBody
{
    public School ?school { get; set; }
    public Classroom ?classroom { get; set; }
    public Student ?student { get; set; }
    public Schoolclasses schoolclass { get; set; }
}