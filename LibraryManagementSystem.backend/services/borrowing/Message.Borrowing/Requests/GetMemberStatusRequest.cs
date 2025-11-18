using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Borrowing.Requests
{
    [Topic("borrowing.record.requests")]
    public record GetMemberStatusRequest(Guid MemberId);
}
