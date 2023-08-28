using CQRS.Core.Events;

namespace SocialMedia.Common.Events
{
    public class PostDeletedEvent : BaseEvent
    {
        public PostDeletedEvent() : base(nameof(PostDeletedEvent))
        { }

        public string Username { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}