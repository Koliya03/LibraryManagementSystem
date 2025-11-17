using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Borrowing.Members
{
    [Topic("catalog.member.activated")]
    public record MemberActivatedMessage(Guid MemberId);
}
