using CQRS.Core.Events;

namespace CQRS.Core.Infrastructure
{
    public interface IEventStore
    {
        public Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
        public Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
    }
}