using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PutController : ControllerBase
{
    [HttpPut("{input}")]
    public int InputPlusOne(int input)
    {
        return input + 1;
    }

    [HttpPut("SumOfInputs")]
    public ActionResult<int> SumInputs([FromBody] SumRequest request)
    {
        return Ok(request.Input1 + request.Input2);
    }
}

public class SumRequest
{
    public int Input1 { get; set; }
    public int Input2 { get; set; }
}