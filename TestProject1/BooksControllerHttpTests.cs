using LibraryInventory.Controllers;
using LibraryInventory.Data;
using LibraryInventory.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace LibraryInventory.TestProject1
{
    [TestClass]
    public class BooksControllerHttpTests
    {
        private Mock<IRepository<Book>> _mockRepository;
        private HttpServer _server;

        [TestInitialize]
        public void Setup()
        {
            // Mock the repository
            _mockRepository = new Mock<IRepository<Book>>();

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

        private HttpClient GetClient() => new HttpClient(_server);

        // Test: GET /api/books
        [TestMethod]
        public void GetBooks_ShouldReturnOkWithBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }
            }.AsQueryable();

            _mockRepository.Setup(repo => repo.GetAll()).Returns(books);

            var client = GetClient();

            // Act
            var response = client.GetAsync("http://localhost/api/books").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var result = response.Content.ReadAsAsync<List<Book>>().Result;
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Book 1", result[0].Title);
        }

        // Test: GET /api/books/{id}
        [TestMethod]
        public void GetBook_ShouldReturnOkWithBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book 1", Author = "Author 1" };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(book);

            var client = GetClient();

            // Act
            var response = client.GetAsync("http://localhost/api/books/1").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var result = response.Content.ReadAsAsync<Book>().Result;
            Assert.AreEqual("Book 1", result.Title);
        }

        // Test: POST /api/books
        [TestMethod]
        public void AddBook_ShouldReturnOk()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "New Book", Author = "New Author" };
            _mockRepository.Setup(repo => repo.Add(It.IsAny<Book>())).Verifiable();

            var client = GetClient();

            // Act
            var response = client.PostAsJsonAsync("http://localhost/api/books", book).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _mockRepository.Verify(repo => repo.Add(It.Is<Book>(b => b.Title == "New Book")), Times.Once);
        }

        // Test: PUT /api/books/{id}
        [TestMethod]
        public void UpdateBook_ShouldReturnOk()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author" };
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Book>())).Verifiable();

            var client = GetClient();

            // Act
            var response = client.PutAsJsonAsync("http://localhost/api/books/1", book).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _mockRepository.Verify(repo => repo.Update(It.Is<Book>(b => b.Id == 1 && b.Title == "Updated Book")), Times.Once);
        }

        // Test: DELETE /api/books/{id}
        [TestMethod]
        public void DeleteBook_ShouldReturnOk()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(new Book { Id = 1 });
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            var client = GetClient();

            // Act
            var response = client.DeleteAsync("http://localhost/api/books/1").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
