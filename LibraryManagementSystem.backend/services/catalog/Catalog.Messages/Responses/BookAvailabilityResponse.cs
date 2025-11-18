using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Catalog.Responses
{
    [Topic("catalog.book.responses")]
    public record BookAvailabilityResponse(
       Guid BookId,
       int AvailableQuantity,
       bool IsAvailable
    );
}
