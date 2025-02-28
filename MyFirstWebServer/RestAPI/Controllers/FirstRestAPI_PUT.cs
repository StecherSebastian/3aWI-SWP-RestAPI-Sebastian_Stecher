using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AddOneToInputController : ControllerBase
{
    [HttpPut("{input}")]
    public int InputPlusOne(int input)
    {
        return input + 1;
    }
}

[ApiController]
[Route("api/[controller]")]
public class SumOfInputsController : ControllerBase
{
    [HttpPut("SumOfInputs")]
    public ActionResult<int> SumInputs([FromBody] SumRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid request body");
        }
        return Ok(request.Input1 + request.Input2);
    }
}

public class SumRequest
{
    public int Input1 { get; set; }
    public int Input2 { get; set; }
}