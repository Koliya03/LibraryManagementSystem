using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public record RegisterMemberDto(
       Guid MemberId,
       string FullName,
       string Email
   );
}
