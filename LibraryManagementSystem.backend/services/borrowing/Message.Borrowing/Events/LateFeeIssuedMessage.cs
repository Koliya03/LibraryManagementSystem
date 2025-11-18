using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Borrowing.Events
{
    [Topic("borrowing.record.lateFees")]
    public record LateFeeIssuedMessage(
       Guid BorrowId,
       decimal FeeAmount
   );
}
