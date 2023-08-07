using CQRS.Core.Domain;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialMedia.Command.Infrastructure.Config;

namespace SocialMedia.Command.Infrastructure.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        public IMongoCollection<EventModel> _eventStoreCollection;

        public EventStoreRepository(IOptions<MongoDbConfig> options)
        {
            MongoClient mongoClient = new(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.Database);

            _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(options.Value.Collection);
        }

        public async Task<List<EventModel>> GetAggregateByIdAsync(string aggregateId)
        {
            return await _eventStoreCollection
                                    .Find(x => x.AggregateIdentifier == aggregateId)
                                    .ToListAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync(EventModel @event)
        {
            await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
        }
    }
}