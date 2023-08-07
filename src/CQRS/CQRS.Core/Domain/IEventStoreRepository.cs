using CQRS.Core.Events;

namespace CQRS.Core.Domain
{
    public interface IEventStoreRepository
    {
        public Task<List<EventModel>> GetAggregateByIdAsync(string aggregateId);
        public Task SaveAsync(EventModel @event);
    }
}