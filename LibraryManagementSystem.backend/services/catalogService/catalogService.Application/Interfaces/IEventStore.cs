using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventStore
    {
        void StartStream<TAggregate>(Guid id, params IDomainEvent[] events) where TAggregate : class;
        void Append(Guid id, params IDomainEvent[] events);
        Task SaveChangesAsync();
        Task<TAggregate?> AggregateAsync<TAggregate>(Guid id)
            where TAggregate : class, new();
    }
}
