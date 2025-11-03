using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Members
{
    public record UpdateMemberStatusCommand(
        Guid Id,
        bool ActiveStatus
    );
}
