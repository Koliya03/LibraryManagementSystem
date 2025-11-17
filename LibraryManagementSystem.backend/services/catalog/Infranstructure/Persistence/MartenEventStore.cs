using Application.Interfaces;
using Domain.Common;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Persistence
{
    public sealed class MartenEventStore : IEventStore
    {
        private readonly IDocumentSession _session;

        public MartenEventStore(IDocumentSession session)
        {
            _session = session;
        }

        public void StartStream<TAggregate>(Guid id, params IDomainEvent[] events)
            where TAggregate : class
        {
            object[] domainEventsToObjEvents = events.Cast<object>().ToArray();
            _session.Events.StartStream<TAggregate>(id, domainEventsToObjEvents);
        }

        public void Append(Guid id, params IDomainEvent[] events)
        {
            object[] domainEventsToObjEvents = events.Cast<object>().ToArray();
            _session.Events.Append(id, domainEventsToObjEvents);
        }
        public Task SaveChangesAsync()
        {
            return _session.SaveChangesAsync();
        }
        public Task<TAggregate?> AggregateAsync<TAggregate>(Guid id)
            where TAggregate : class, new()
        {
            return _session.Events.AggregateStreamAsync<TAggregate>(id);
        }

        public async Task<TAggregate?> LoadAsync<TAggregate>(Guid id)
            where TAggregate : class, new()
        {
            var aggregate = await _session.LoadAsync<TAggregate>(id);
            return aggregate;
        }
    }
}
