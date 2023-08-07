using CQRS.Core.Commands;

namespace CQRS.Core.Infrastructure
{
    public interface ICommandDispatcher
    {
        public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;
        public Task SendAsync(BaseCommand command);
    }
}