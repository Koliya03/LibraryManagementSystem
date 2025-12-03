using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BorrowedBookDto
    {
        public Guid BookId { get; set; }
        public int TimesBorrowed { get; set; }

        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string ISBN { get; set; } = "";
    }
}
