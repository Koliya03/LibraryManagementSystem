using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Members.Events
{
    public record MemberRegisteredEvent(
        Guid MemberId,
        string FullName,
        string Email
    );
}
