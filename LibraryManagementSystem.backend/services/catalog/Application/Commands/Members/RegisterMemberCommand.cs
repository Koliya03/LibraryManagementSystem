using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Members
{
    public record RegisterMemberCommand(
        Guid MemberId,
        string FullName ,
        string Email 
    );
}
