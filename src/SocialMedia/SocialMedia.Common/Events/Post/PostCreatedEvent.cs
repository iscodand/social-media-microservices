using CQRS.Core.Events;

namespace SocialMedia.Common.Events
{
    public class PostCreatedEvent : BaseEvent
    {
        public PostCreatedEvent() : base(nameof(PostCreatedEvent))
        { }

        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}