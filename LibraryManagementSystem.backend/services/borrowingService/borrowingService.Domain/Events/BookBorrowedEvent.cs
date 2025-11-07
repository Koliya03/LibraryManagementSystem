using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public record BookBorrowedEvent(
        Guid BorrowId,
        Guid MemberId,
        Guid BookId,
        int Quantity,
        DateTime BorrowDate
     ) : IDomainEvent;
}
