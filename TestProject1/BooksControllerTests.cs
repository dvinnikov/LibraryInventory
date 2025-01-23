using LibraryInventory.Controllers;
using LibraryInventory.Data;
using LibraryInventory.Models;
using Moq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Unity.Lifetime;
using Unity;


namespace LibraryInventory.TestProject1
{
    [TestClass]
    public class BooksControllerTests
    {
        private Mock<IRepository<Book>> _mockRepository;
        private BooksController _controller;
        private HttpServer _server;

        [TestInitialize]
        public void Setup()
        {
            // Mock the repository
            _mockRepository = new Mock<IRepository<Book>>();

            // Initialize the controller with the mock repository
            _controller = new BooksController(_mockRepository.Object);

            // Configure Unity container
            var container = new UnityContainer();
            container.RegisterInstance(_mockRepository.Object, new ContainerControlledLifetimeManager());

            // Set up Web API configuration
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Set Unity as the dependency resolver
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            // Initialize the HTTP server
            _server = new HttpServer(config);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _server.Dispose();
        }

        private HttpClient GetClient()
        {
            return new HttpClient(_server)
            {
                BaseAddress = new Uri("http://localhost/")
            };
        }

        [TestMethod]
        public void GetBooks_ShouldReturnPagedBooks_WhenCalled()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3" }
            }.AsQueryable();

            _mockRepository.Setup(repo => repo.GetAll()).Returns(books);

            // Act
            var result = _controller.GetBooks(page: 1, pageSize: 2);

            // Assert
            Assert.IsNotNull(result, "Result is null. The controller did not return a valid response.");
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Book>>), "Result is not of the expected type.");

            var okResult = result as OkNegotiatedContentResult<List<Book>>;
            Assert.IsNotNull(okResult, "Failed to cast result to OkNegotiatedContentResult.");
            var returnedBooks = okResult.Content;
            Assert.AreEqual(2, returnedBooks.Count);
            Assert.AreEqual("Book 1", returnedBooks[0].Title);
            Assert.AreEqual("Book 2", returnedBooks[1].Title);
        }





        [TestMethod]
        public void GetBook_ShouldReturnCorrectBook_WhenIdIsValid()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author" };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(book);

            // Act
            var result = _controller.GetBook(1) as OkNegotiatedContentResult<Book>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Book", result.Content.Title);
        }

        [TestMethod]
        public void AddBook_ShouldCallAddMethod_WithCorrectBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "New Book", Author = "New Author" };

            // Act
            var result = _controller.AddBook(book) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            _mockRepository.Verify(repo => repo.Add(book), Times.Once);
        }

        [TestMethod]
        public void UpdateBook_ShouldCallUpdateMethod_WithCorrectBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author" };

            // Act
            var result = _controller.UpdateBook(1, book) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            _mockRepository.Verify(repo => repo.Update(It.Is<Book>(b => b.Id == 1 && b.Title == "Updated Book")), Times.Once);
        }

        [TestMethod]
        public void DeleteBook_ShouldCallDeleteMethod_WithCorrectId()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book" };

            _mockRepository.Setup(repo => repo.GetById(1)).Returns(book);
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            var client = GetClient();

            // Act
            var response = client.DeleteAsync("http://localhost/api/books/1").Result;

            // Assert
            Assert.IsNotNull(response); // Ensure the response is not null
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode); // Check for 200 OK
            _mockRepository.Verify(repo => repo.Delete(1), Times.Once); // Verify Delete is called
        }
    }
}
