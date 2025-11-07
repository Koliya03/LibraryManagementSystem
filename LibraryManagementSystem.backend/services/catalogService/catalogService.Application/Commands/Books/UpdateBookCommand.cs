using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Books
{
    public record UpdateBookCommand(
        Guid BookId,
        string Title,
        string Author,
        string ISBN,
        int TotalQuantity,
        int AvailableQuantity
   );
}
