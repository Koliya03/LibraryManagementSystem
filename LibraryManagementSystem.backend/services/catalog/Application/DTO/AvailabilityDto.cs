using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AvailabilityDto
    {
        public Guid BookId { get; set; }
        public int AvailableQuantity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
