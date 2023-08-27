using CQRS.Core.Commands;

namespace SocialMedia.Command.WebApi.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public string Comment { get; set; }
        public string Username { get; set; }
    }
}