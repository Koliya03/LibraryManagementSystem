using Application.Interfaces;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Persistence
{
    public class MartenReadStore : IReadStore
    {
        private readonly IQuerySession _query;

        public MartenReadStore(IQuerySession query)
        {
            _query = query;
        }

        public Task<T?> LoadAsync<T>(Guid id) where T : class
        {
            return _query.LoadAsync<T>(id);
        }

        public async Task<List<T>> ListAsync<T>() where T : class
        {
            var items = await _query.Query<T>().ToListAsync();
            return items.ToList();
        }
    }
}
