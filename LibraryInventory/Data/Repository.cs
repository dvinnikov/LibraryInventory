using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryInventory.Data
{
    /// <summary>
    /// A generic base class for data access operations on entities.
    /// Encapsulates common repository functionality and interacts with the database context.
    /// </summary>
    /// <typeparam name="T">The type of entity this repository manages.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(LibraryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T GetById(int id) => _dbSet.Find(id);

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingEntity = _dbSet.Find(GetEntityKey(entity));
            if (existingEntity == null)
                throw new InvalidOperationException("Entity not found.");

            // Copy values manually or use a utility method
            foreach (var property in typeof(T).GetProperties())
            {
                var newValue = property.GetValue(entity);
                property.SetValue(existingEntity, newValue);
            }

            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                throw new ArgumentException($"No entity found with id {id}.");

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        private object GetEntityKey(T entity)
        {
            var keyProperty = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                     p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true).Any());

            if (keyProperty == null)
                throw new InvalidOperationException($"No key property found for entity type {typeof(T).Name}.");

            return keyProperty.GetValue(entity);
        }
    }
}