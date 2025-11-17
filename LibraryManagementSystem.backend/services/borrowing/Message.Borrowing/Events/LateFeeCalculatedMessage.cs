using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Borrowing.Events
{
    public record LateFeeIssuedMessage(
       Guid BorrowId,
       decimal FeeAmount
   );
}
