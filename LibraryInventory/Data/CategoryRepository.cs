using LibraryInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LibraryInventory.Data
{
    /// <summary>
    /// Provides data access operations for managing categories in the library system.
    /// Supports paginated retrieval and custom SQL query execution.
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ILibraryQueryExecutor _queryExecutor;

        public CategoryRepository(LibraryContext context, ILibraryQueryExecutor queryExecutor) : base(context)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public IEnumerable<Category> GetPagedCategories(int page, int pageSize, string search, string sortColumn, string sortOrder)
        {
            var parameters = new[]
            {
            new SqlParameter("@Page", page),
            new SqlParameter("@PageSize", pageSize),
            new SqlParameter("@Search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search),
            new SqlParameter("@SortColumn", sortColumn),
            new SqlParameter("@SortOrder", sortOrder)
        };

            return _queryExecutor.ExecuteSqlQuery<Category>("EXEC GetPagedCategories @Page, @PageSize, @Search, @SortColumn, @SortOrder", parameters);
        }
    }


}