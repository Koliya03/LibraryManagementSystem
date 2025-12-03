using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record BorrowedRecord(
        Guid Id,
        Guid MemberId,
        Guid BookId,
        DateTime BorrowDate,
        DateTime DueDate,
        DateTime? ReturnDate,
        bool IsReturned,
        bool IsLost,
        decimal LateFee
    );
}
