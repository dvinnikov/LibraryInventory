using LibraryInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInventory.Data
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetPagedCategories(int page, int pageSize, string search, string sortColumn, string sortOrder);
    }

}
