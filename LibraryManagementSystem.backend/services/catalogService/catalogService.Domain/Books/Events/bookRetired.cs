using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books.Events
{
    public record BookRetiredEvent(
        Guid BookId
    );
}
