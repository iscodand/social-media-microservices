
using SocialMedia.Common.Events;

namespace SocialMedia.Query.Infrastructure.Handlers
{
    public interface IEventHandler
    {
        public Task On(PostCreatedEvent @event);
        public Task On(PostUpdatedEvent @event);
        public Task On(PostLikedEvent @event);
        public Task On(PostDeletedEvent @event);
        public Task On(CommentAddedEvent @event);
        public Task On(CommentUpdatedEvent @event);
        public Task On(CommentRemovedEvent @event);
    }
}