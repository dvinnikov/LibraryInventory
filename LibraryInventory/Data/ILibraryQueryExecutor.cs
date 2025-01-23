using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInventory.Data
{
    public interface ILibraryQueryExecutor
    {
        IEnumerable<T> ExecuteSqlQuery<T>(string sql, params object[] parameters);
    }
}
