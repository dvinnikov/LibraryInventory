using LibraryInventory.Data;
using Moq;
using System.Data.Entity;

namespace TestProject1
{
    [TestClass]
    public class RepositoryTests
    {
        private Mock<LibraryContext> _mockContext;
        private Mock<DbSet<TestEntity>> _mockDbSet;
        private Repository<TestEntity> _repository;
        private List<TestEntity> _data;

        [TestInitialize]
        public void Setup()
        {
            _data = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Entity1" },
                new TestEntity { Id = 2, Name = "Entity2" }
            };

            var queryableData = _data.AsQueryable();

            _mockDbSet = new Mock<DbSet<TestEntity>>();
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            _mockDbSet.Setup(m => m.Add(It.IsAny<TestEntity>())).Callback<TestEntity>(_data.Add);
            _mockDbSet.Setup(m => m.Remove(It.IsAny<TestEntity>())).Callback<TestEntity>(e => _data.Remove(e));
            _mockDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _data.SingleOrDefault(e => e.Id == (int)ids[0]));

            _mockContext = new Mock<LibraryContext>();
            _mockContext.Setup(c => c.Set<TestEntity>()).Returns(_mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            _repository = new Repository<TestEntity>(_mockContext.Object);
        }



        [TestMethod]
        public void GetAll_ShouldReturnAllEntities()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectEntity()
        {
            // Act
            var result = _repository.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Entity1", result.Name);
        }


        [TestMethod]
        public void Add_ShouldAddEntity()
        {
            // Arrange
            var newEntity = new TestEntity { Id = 3, Name = "Entity3" };

            // Act
            _repository.Add(newEntity);

            // Assert
            Assert.AreEqual(3, _data.Count);
            Assert.AreEqual(newEntity, _data.Last());
        }

        [TestMethod]
        public void Update_ShouldModifyEntity()
        {
            // Arrange
            var updatedEntity = new TestEntity { Id = 1, Name = "UpdatedEntity1" };

            // Act
            _repository.Update(updatedEntity);

            // Assert
            var entity = _data.First(e => e.Id == 1);
            Assert.AreEqual("UpdatedEntity1", entity.Name);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }


        [TestMethod]
        public void Delete_ShouldRemoveEntity()
        {
            // Act
            _repository.Delete(1);

            // Assert
            Assert.AreEqual(1, _data.Count);
            Assert.IsFalse(_data.Any(e => e.Id == 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_NonExistentEntity_ShouldThrowException()
        {
            // Act
            _repository.Delete(99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullEntity_ShouldThrowException()
        {
            // Act
            _repository.Add(null);
        }
    }

    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
