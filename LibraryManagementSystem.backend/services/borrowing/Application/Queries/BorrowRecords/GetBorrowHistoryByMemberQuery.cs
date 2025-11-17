using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.BorrowRecords
{
    public record GetBorrowHistoryByMemberQuery(Guid MemberId);
}
