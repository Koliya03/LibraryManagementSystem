using Domain.Books.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task<Book?> GetByIdAsync(Guid id);
        Task<List<Book>> GetAllAsync();
        Task UpdateBookAsync(Book book);
    }
}
