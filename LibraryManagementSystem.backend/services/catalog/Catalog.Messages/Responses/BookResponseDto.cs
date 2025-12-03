using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Catalog.Responses
{
    public record BookResponseDto(
        Guid Id,
        string Title,
        string Author,
        string ISBN,
        int TotalQuantity,
        int AvailableQuantity,
        bool IsRetired
    );
}
