using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;


namespace Messages.Borrowing.Books
{
    [Topic("catalog.book.registered")]
    public record BookRegisteredMessage(
        Guid BookId,
        string Title,
        string Author,
        string ISBN,
        int TotalQuantity,
        int AvailableQuantity,
        bool IsRetired
    );
}
