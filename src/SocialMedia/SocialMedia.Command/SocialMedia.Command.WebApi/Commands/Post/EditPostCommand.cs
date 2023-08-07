using CQRS.Core.Commands;

namespace SocialMedia.Command.WebApi.Commands
{
    public class EditPostCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}