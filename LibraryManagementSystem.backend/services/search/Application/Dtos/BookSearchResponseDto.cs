using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class BookSearchResponseDto
    {
        public Guid BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public int TotalQuantity { get; set; }

        public int AvailableQuantity { get; set; }

        public bool IsRetired { get; set; }
    }
}
