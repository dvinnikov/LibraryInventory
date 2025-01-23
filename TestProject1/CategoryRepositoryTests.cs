using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using LibraryInventory.Data;
using LibraryInventory.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class CategoryRepositoryTests
{
    private Mock<ILibraryQueryExecutor> _mockQueryExecutor;
    private Mock<LibraryContext> _mockContext;
    private CategoryRepository _repository;

    [TestInitialize]
    public void TestInitialize()
    {
        // Mock data
        var data = new List<Category>
        {
            new Category { Id = 1, Name = "Fiction" },
            new Category { Id = 2, Name = "Non-Fiction" }
        };

        // Mock query executor
        _mockQueryExecutor = new Mock<ILibraryQueryExecutor>();
        _mockQueryExecutor
            .Setup(q => q.ExecuteSqlQuery<Category>(
                It.IsAny<string>(),
                It.IsAny<object[]>()
            ))
            .Returns(data);

        // Mock LibraryContext (if required by Repository<T>)
        _mockContext = new Mock<LibraryContext>();

        // Initialize repository
        _repository = new CategoryRepository(_mockContext.Object, _mockQueryExecutor.Object);
    }

    [TestMethod]
    public void GetPagedCategories_ValidInput_ReturnsCategories()
    {
        // Arrange
        const int page = 1, pageSize = 10;
        const string sortColumn = "Name", sortOrder = "ASC";

        // Act
        var result = _repository.GetPagedCategories(page, pageSize, null, sortColumn, sortOrder);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Fiction", result.First().Name);
    }
}