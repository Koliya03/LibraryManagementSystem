using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Borrowing.Response
{
    public record BorrowHistoryResponse(List<BorrowRecordResponseDto> Records);
}
