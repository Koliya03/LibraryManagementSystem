using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReadModels
{
    public class BookReadModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public bool IsRetired { get; set; }
    }
}
