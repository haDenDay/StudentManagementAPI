using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private static List<Product> products = new()
		{
			new Product { Id = Guid.NewGuid(), Name = "Áo thun", Price = 150000, Description = "Áo thun cotton chất lượng cao" },
			new Product { Id = Guid.NewGuid(), Name = "Áo sơ mi", Price = 250000, Description = "Áo sơ mi công sở" }
		};

		
		[HttpGet("/products")]
		public IActionResult GetAllProducts() => Ok(products);


		[HttpGet("/products/{id}")]
		public IActionResult GetProductById(Guid id)
		{
			var product = products.FirstOrDefault(p => p.Id == id);
			return product == null ? NotFound() : Ok(product);
		}

	
		[HttpGet("search")]
		public IActionResult SearchProduct(string name)
		{
			var result = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
			return result.Count == 0 ? NotFound("Không tìm thấy sản phẩm.") : Ok(result);
		}

		
		[HttpPost]
		public IActionResult CreateProduct([FromBody] Product product)
		{
			product.Id = Guid.NewGuid();
			products.Add(product);
			return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateProduct(Guid id, [FromBody] Product updatedProduct)
		{
			var product = products.FirstOrDefault(p => p.Id == id);
			if (product == null) return NotFound();

			product.Name = updatedProduct.Name;
			product.Price = updatedProduct.Price;
			product.Description = updatedProduct.Description;
			return NoContent();
		}


		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(Guid id)
		{
			var product = products.FirstOrDefault(p => p.Id == id);
			if (product == null) return NotFound();

			products.Remove(product);
			return NoContent();
		}
	}
}
