using LibraryInventory.Data;
using LibraryInventory.Models;
using System.Web.Http;

namespace LibraryInventory.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private readonly IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCategories()
        {
            var categories = _repository.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Category cannot be null.");

            _repository.Add(category);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null || id != category.Id)
                return BadRequest("Invalid category data.");

            _repository.Update(category);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }

}
