using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReadStore
    {
        Task<T?> LoadAsync<T>(Guid id) where T : class;
        Task<List<T>> ListAsync<T>() where T : class;
    }
}
