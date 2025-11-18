using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Catalog.Events.Books
{
    [Topic("catalog.book.updated")]
    public record BookUpdatedMessage(
       Guid BookId,
       string Title,
       string Author,
       string ISBN,
       int TotalQuantity, 
       int AvailableQuantity
   );
}
