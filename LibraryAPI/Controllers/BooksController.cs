using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private static List<Book> books = new List<Book>
		{
			new Book { Id = Guid.NewGuid(), Title = "C# Cơ bản", Author = "Nguyễn Văn A", ISBN = "123456789" },
			new Book { Id = Guid.NewGuid(), Title = "ASP.NET Core", Author = "Trần Thị B", ISBN = "987654321" }
		};

		[HttpGet]
		public IActionResult GetAllBooks() => Ok(books);

		[HttpGet("{id}")]
		public IActionResult GetBookById(Guid id)
		{
			var book = books.FirstOrDefault(b => b.Id == id);
			return book == null ? NotFound() : Ok(book);
		}

		[HttpPost]
		public IActionResult CreateBook([FromBody] Book newBook)
		{
			newBook.Id = Guid.NewGuid();
			books.Add(newBook);
			return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateBook(Guid id, [FromBody] Book updatedBook)
		{
			var book = books.FirstOrDefault(b => b.Id == id);
			if (book == null) return NotFound();

			book.Title = updatedBook.Title;
			book.Author = updatedBook.Author;
			book.ISBN = updatedBook.ISBN;

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBook(Guid id)
		{
			var book = books.FirstOrDefault(b => b.Id == id);
			if (book == null) return NotFound();

			books.Remove(book);
			return NoContent();
		}
	}
}
