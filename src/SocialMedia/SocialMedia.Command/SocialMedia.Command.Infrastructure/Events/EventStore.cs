using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using SocialMedia.Command.Domain.Aggregates;

namespace SocialMedia.Command.Infrastructure.Events
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _eventProducer;

        public EventStore(IEventStoreRepository eventStoreRepository,
                                     IEventProducer eventProducer)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventProducer = eventProducer;
        }

        public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
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

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
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

                // Producing event to Kafka
                string topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                await _eventProducer.ProduceAsync(topic, @event);
            }
        }
    }
}