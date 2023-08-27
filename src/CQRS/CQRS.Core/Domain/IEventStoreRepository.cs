using CQRS.Core.Events;

namespace CQRS.Core.Domain
{
    public interface IEventStoreRepository
    {
        public Task<List<EventModel>> GetAggregateByIdAsync(Guid aggregateId);
        public Task SaveAsync(EventModel @event);
    }
}