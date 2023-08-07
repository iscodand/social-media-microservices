using CQRS.Core.Commands;

namespace SocialMedia.Command.WebApi.Commands
{
    public class RemoveCommentCommand : BaseCommand
    {
        public Guid CommentId { get; set; }
        public string Username { get; set; }
    }
}