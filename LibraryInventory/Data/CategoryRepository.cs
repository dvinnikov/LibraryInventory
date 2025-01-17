using LibraryInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryInventory.Data
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly LibraryContext _context;

        public CategoryRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetPagedCategories(int page, int pageSize, string search, string sortColumn, string sortOrder)
        {
            var pageParam = new SqlParameter("@Page", page);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);
            var searchParam = new SqlParameter("@Search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);
            var sortColumnParam = new SqlParameter("@SortColumn", sortColumn);
            var sortOrderParam = new SqlParameter("@SortOrder", sortOrder);

            return _context.Database.SqlQuery<Category>(
                "EXEC GetPagedCategories @Page, @PageSize, @Search, @SortColumn, @SortOrder",
                pageParam, pageSizeParam, searchParam, sortColumnParam, sortOrderParam
            ).ToList();
        }
    }
}