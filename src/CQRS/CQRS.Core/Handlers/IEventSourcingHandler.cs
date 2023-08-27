using CQRS.Core.Domain;

namespace CQRS.Core.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        public Task SaveAsync(AggregateRoot aggregate);
        public Task<T> GetAggregateById(Guid aggregateId);
    }
}