using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books.Events
{
    public record BookLostEvent(Guid BookId) : IDomainEvent;
}
