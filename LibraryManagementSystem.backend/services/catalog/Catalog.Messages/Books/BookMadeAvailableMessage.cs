using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Catalog.Books
{
    [Topic("catalog.book.madeAvailable")]
    public record BookMadeAvailableMessage(Guid BookId);
}
