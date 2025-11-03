using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Books
{
    public record RegisterBookCommand(
        string Title,
        string Author,
        string ISBN,
        int TotalQuantity
    );
}
