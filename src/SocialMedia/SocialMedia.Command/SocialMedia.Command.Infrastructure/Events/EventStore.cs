using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Infrastructure;

namespace SocialMedia.Command.Infrastructure.Events
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public Task<List<BaseEvent>> GetEventsAsync(string aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task SaveEventsAsync(string aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            throw new NotImplementedException();
        }
    }
}