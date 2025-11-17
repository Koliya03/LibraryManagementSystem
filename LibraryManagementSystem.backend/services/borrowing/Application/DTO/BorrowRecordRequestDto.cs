using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class BorrowRecordRequestDto
    {
        public Guid BorrowId { get; set; }
        public Guid MemberId { get; set; }
        public Guid BookId { get; set; }
    }
}
