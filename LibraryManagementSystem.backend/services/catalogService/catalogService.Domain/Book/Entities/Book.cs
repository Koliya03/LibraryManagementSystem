using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Book.Entities
{
    public class Book
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string ISBN { get; set; }
        public int totalQuantity { get; set; }
        public int availableQuantity { get; set; }
        public bool isRetired { get; set; }
    }
}
