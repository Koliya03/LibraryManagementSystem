using Application.Interfaces;
using Domain.Book.Entities;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Repositories
{
    public class bookRepository:IBookRepository
    {
        private readonly IDocumentSession _session;

        public bookRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task AddBookAsync(Book book)
        {
           _session.Store(book);
            await _session.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return (await _session.Query<Book>().ToListAsync()).ToList();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _session.LoadAsync<Book>(id);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _session.Store(book);
            await _session.SaveChangesAsync();
        }
    }
}
