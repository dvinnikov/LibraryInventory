using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryInventory.Data
{
    /// <summary>
    /// Defines an interface for executing raw SQL queries.
    /// Provides methods for retrieving data using custom SQL commands.
    /// </summary>
    public class LibraryQueryExecutor : ILibraryQueryExecutor
    {
        private readonly LibraryContext _context;

        public LibraryQueryExecutor(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<T> ExecuteSqlQuery<T>(string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(sql, parameters).ToList();
        }
    }

}