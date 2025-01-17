using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryInventory.Data
{
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
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                throw new ArgumentException($"No entity found with id {id} to delete.");

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

    }
}