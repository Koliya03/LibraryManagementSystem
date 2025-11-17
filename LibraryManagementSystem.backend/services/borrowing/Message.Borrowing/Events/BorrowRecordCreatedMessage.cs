using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Borrowing.Events
{
    [Topic("borrowing.record.created")]
    public record BorrowRecordCreatedMessage(
       Guid BorrowId,
       Guid MemberId,
       Guid BookId,
       DateTime BorrowDate,
       DateTime DueDate
   );
}
