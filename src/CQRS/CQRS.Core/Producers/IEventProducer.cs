using CQRS.Core.Events;

namespace CQRS.Core.Producers
{
    public interface IEventProducer
    {
        public Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent;
    }
}