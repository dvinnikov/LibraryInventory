using LibraryInventory.Data;
using LibraryInventory.Models;
using System.Linq;
using System.Web.Http;

namespace LibraryInventory.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private readonly IRepository<Book> _repository;

        public BooksController(IRepository<Book> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBooks(int page = 1, int pageSize = 10, string search = null, string sortColumn = "Title", string sortOrder = "asc")
        {
            var books = _repository.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
            }

            // Apply dynamic sorting using reflection
            if (!string.IsNullOrEmpty(sortColumn))
            {
                books = sortOrder == "asc"
                    ? books.OrderBy(b => GetPropertyValue(b, sortColumn))
                    : books.OrderByDescending(b => GetPropertyValue(b, sortColumn));
            }

            var pagedBooks = books.Skip((page - 1) * pageSize).Take(pageSize);

            return Ok(pagedBooks);
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }



        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetBook(int id) => Ok(_repository.GetById(id));

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBook([FromBody] Book book)
        {
            _repository.Add(book);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateBook(int id, [FromBody] Book book)
        {
            book.Id = id;
            _repository.Update(book);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteBook(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
