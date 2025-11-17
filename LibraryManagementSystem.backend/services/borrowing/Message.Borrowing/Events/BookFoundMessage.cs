using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Borrowing.Events
{
    [Topic("borrowing.record.found")]
    public record BookFoundMessage(
        Guid BorrowId,
        Guid MemberId,
        Guid BookId,
        DateTime ReportedDate
    );
}
