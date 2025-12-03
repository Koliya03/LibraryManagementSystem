using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Report
{
    [Topic("reporting.borrowing.requests")]
    public record GetAllBorrowRecordsRequest();
}
