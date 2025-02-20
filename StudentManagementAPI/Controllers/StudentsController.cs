using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		
		private static List<Student> students = new List<Student>
		{
			new Student { Id = Guid.NewGuid(), Name = "Nguyễn Văn A", Age = 20 },
			new Student { Id = Guid.NewGuid(), Name = "Trần Thị B", Age = 21 }
		};

	
		[HttpGet]
		public ActionResult<IEnumerable<Student>> GetAllStudents()
		{
			return Ok(students);
		}


		[HttpGet("{id}")]
		public ActionResult<Student> GetStudentById(Guid id)
		{
			var student = students.FirstOrDefault(s => s.Id == id);
			if (student == null)
				return NotFound();
			return Ok(student);
		}

		
		[HttpPost]
		public ActionResult<Student> CreateStudent([FromBody] Student newStudent)
		{
			newStudent.Id = Guid.NewGuid();
			students.Add(newStudent);
			return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
		}


		[HttpPut("{id}")]
		public IActionResult UpdateStudent(Guid id, [FromBody] Student updatedStudent)
		{
			var student = students.FirstOrDefault(s => s.Id == id);
			if (student == null)
				return NotFound();

			student.Name = updatedStudent.Name;
			student.Age = updatedStudent.Age;

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteStudent(Guid id)
		{
			var student = students.FirstOrDefault(s => s.Id == id);
			if (student == null)
				return NotFound();

			students.Remove(student);
			return NoContent();
		}
	}
}

