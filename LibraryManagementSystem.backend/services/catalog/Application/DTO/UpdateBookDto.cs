using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public record UpdateBookDto(
       Guid BookId,
       string Title,
       string Author,
       string ISBN,
       int TotalQuantity,
       int AvailableQuantity
  );
}
