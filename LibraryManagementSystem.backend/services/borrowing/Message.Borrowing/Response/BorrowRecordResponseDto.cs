using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Borrowing.Response
{
    public record BorrowRecordResponseDto(
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
