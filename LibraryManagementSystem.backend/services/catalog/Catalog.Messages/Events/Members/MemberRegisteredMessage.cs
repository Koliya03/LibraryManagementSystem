using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Catalog.Events.Members
{
    [Topic("catalog.member.registered")]
    public record MemberRegisteredMessage(Guid MemberId, string FullName, string Email,bool IsActive );
}

