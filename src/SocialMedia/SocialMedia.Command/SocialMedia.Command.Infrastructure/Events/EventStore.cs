using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using SocialMedia.Command.Domain.Aggregates;

namespace SocialMedia.Command.Infrastructure.Events
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<List<BaseEvent>> GetEventsAsync(string aggregateId)
        {
            List<EventModel> eventStream = await _eventStoreRepository.GetAggregateByIdAsync(aggregateId);

            if (eventStream == null || !eventStream.Any())
            {
                throw new AggregateNotFoundException("This aggregate don't exists in the current context");
            }

            List<BaseEvent> eventData = eventStream.OrderBy(x => x.Version)
                                                   .Select(x => x.EventData)
                                                   .ToList();

            return eventData;
        }

        public async Task SaveEventsAsync(string aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            List<EventModel> eventStream = await _eventStoreRepository.GetAggregateByIdAsync(aggregateId);

            if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            {
                throw new ConcurrencyException();
            }

            int version = expectedVersion;

            foreach (BaseEvent @event in events)
            {
                version++;
                @event.Version = version;
                string eventType = @event.GetType().Name;

                EventModel eventModel = new()
                {
                    TimeStamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(PostAggregate),
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };

                await _eventStoreRepository.SaveAsync(eventModel);
            }
        }
    }
}