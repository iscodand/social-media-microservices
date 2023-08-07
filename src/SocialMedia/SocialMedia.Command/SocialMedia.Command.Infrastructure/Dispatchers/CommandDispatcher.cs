using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;

namespace SocialMedia.Command.Infrastructure.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();

        public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                throw new IndexOutOfRangeException("You cannot register same command twice!");
            }

            _handlers.Add(typeof(T), x => handler((T)x));
        }

        public async Task SendAsync(BaseCommand command)
        {
            if (!_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
            {
                throw new ArgumentNullException(nameof(handler), "No command handler was registered");
            }

            await handler(command);
        }
    }
}