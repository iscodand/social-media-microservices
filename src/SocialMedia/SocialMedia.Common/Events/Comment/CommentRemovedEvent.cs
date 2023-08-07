using CQRS.Core.Events;

namespace SocialMedia.Common.Events
{
    public class CommentRemovedEvent : BaseEvent
    {
        public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
        { }

        public Guid CommentId { get; set; }
        public string Username { get; set; }
        public DateTime RemovedAt { get; set; }
    }
}