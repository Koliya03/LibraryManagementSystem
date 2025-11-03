using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books.Events
{
    public record BookRegisteredEvent(
       Guid BookId,
       string Title,
       string Author,
       string ISBN,
       int TotalQuantity
   );
}
