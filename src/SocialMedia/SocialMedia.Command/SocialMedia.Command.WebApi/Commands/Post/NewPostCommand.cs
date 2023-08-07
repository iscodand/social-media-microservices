using CQRS.Core.Commands;

namespace SocialMedia.Command.WebApi.Commands
{
    public class NewPostCommand : BaseCommand
    {
        public string Author { get; set; }
        public string Message { get; set; }
    }
}