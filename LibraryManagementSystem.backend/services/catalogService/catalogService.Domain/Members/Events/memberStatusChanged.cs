using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Member.Events
{
    
    public record MemberStatusChangedEvent(
        Guid MemberId,
        bool IsActive
    );
}
