using CQRS.Core.Events;

namespace SocialMedia.Common.Events
{
    public class PostLikedEvent : BaseEvent
    {
        public PostLikedEvent() : base(nameof(PostLikedEvent))
        { }

        public DateTime CreatedAt { get; set; } // == LikedAt
    }
}