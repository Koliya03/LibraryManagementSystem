using Domain.Events;
using Marten.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BorrowRecord
    {
        [Identity] public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsLost { get; set; }
        public decimal LateFee { get; set; }
    }
}
