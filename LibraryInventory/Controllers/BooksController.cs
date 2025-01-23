using LibraryInventory.Data;
using LibraryInventory.Models;
using System;
using System.Linq;
using System.Net;
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

            if (!string.IsNullOrEmpty(sortColumn))
            {
                books = sortOrder == "asc"
                    ? books.OrderBy(b => GetPropertyValue(b, sortColumn))
                    : books.OrderByDescending(b => GetPropertyValue(b, sortColumn));
            }

            // Pagination logic
            var pagedBooks = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Return Ok with the paginated result
            return Ok(pagedBooks); // Ensure this is reached
        }


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetBook(int id)
        {
            try
            {
                var book = _repository.GetById(id);
                if (book == null)
                    return NotFound(); // 404 Not Found

                return Ok(book);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // 500 Internal Server Error
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book data is required."); // 400 Bad Request

            try
            {
                _repository.Add(book);
                return Ok(); // 200 OK
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book data is required."); // 400 Bad Request

            try
            {
                book.Id = id;
                _repository.Update(book);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteBook(int id)
        {
            try
            {
                var book = _repository.GetById(id);
                if (book == null)
                    return NotFound(); // 404 Not Found

                _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }
    }
}
