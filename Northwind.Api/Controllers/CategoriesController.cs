using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Domain;
using Northwind.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly IEntityService<Category> _service;

        public CategoriesController(IEntityService<Category> service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories() =>
            await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id) {
            var category = await _service.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category) {
            category.State = State.Added;
            int affectedEntries = await _service.SaveAsync(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category) {
            if (id != category.Id)
                return BadRequest();

            if (!_service.Exists(id))
                return NotFound();

            category.State = State.Modified;
            int affectedEntries = await _service.SaveAsync(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) {
            var category = await _service.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            category.State = State.Deleted;
            int affectedEntries = await _service.SaveAsync(category);

            return NoContent();
        }
    }
}
