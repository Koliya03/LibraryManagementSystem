using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Book.Events
{
    public record BookUpdatedEvent(
        Guid BookId,
        string Title,
        string Author,
        int TotalQuantity,
        int AvailableQuantity
    );
}
