using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public record BookMarkedLostEvent(
        Guid BorrowId,
        Guid MemberId,
        Guid BookId,
        int Quantity,
        DateTime ReportedDate
    ) : IDomainEvent;
}
