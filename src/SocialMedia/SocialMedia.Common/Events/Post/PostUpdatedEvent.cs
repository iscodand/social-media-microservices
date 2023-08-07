using CQRS.Core.Events;

namespace SocialMedia.Common.Events
{
    public class PostUpdatedEvent : BaseEvent
    {
        public PostUpdatedEvent() : base(nameof(PostUpdatedEvent))
        { }

        public string Message { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}