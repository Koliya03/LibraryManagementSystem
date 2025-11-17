using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Borrowing.Requests
{
    public record GetBookAvailabilityRequest(Guid BookId);
}
