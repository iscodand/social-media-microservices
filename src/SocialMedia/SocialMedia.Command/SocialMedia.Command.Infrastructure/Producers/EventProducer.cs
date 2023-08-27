using System.Text.Json;
using Confluent.Kafka;
using CQRS.Core.Events;
using CQRS.Core.Producers;
using Microsoft.Extensions.Options;

namespace SocialMedia.Command.Producers
{
    public class EventProducer : IEventProducer
    {
        private readonly ProducerConfig _config;

        public EventProducer(IOptions<ProducerConfig> config)
        {
            _config = config.Value;
        }

        public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
        {
            using var producer = new ProducerBuilder<string, string>(_config)
                        .SetKeySerializer(Serializers.Utf8)
                        .SetValueSerializer(Serializers.Utf8)
                        .Build();

            Message<string, string> message = new()
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };

            DeliveryResult<string, string> produceResult = await producer.ProduceAsync(topic, message);

            if (produceResult.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Could not produce {@event.GetType().ToString()} message to topic {topic} because: {produceResult.Message}");
            }
        }
    }
}