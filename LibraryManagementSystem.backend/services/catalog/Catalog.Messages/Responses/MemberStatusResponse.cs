using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Catalog.Responses
{
    [Topic("catalog.member.responses")]
    public record MemberStatusResponse(
        Guid MemberId,
        bool IsActive
    );
}
