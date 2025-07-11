using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    
    [HttpGet]
    public IActionResult GetAllStudents()
    {
      // This method would typically retrieve a list of students from a database
      // For now, we will return a placeholder response
      var students = new List<string> { "Alice", "Bob", "Charlie", "David", "Eve" };
      return Ok(students);
    }
    }
}
