using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISearchIndex<T> where T : class
    {
        Task IndexAsync(T document);

        Task<T?> GetByIdAsync(Guid id);

        Task<List<T>> SearchAsync(string query);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(T document);
    }
}
