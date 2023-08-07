using CQRS.Core.Commands;

namespace SocialMedia.Command.WebApi.Commands
{
    public class EditCommentCommand : BaseCommand
    {
        public Guid CommentId { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
    }
}