using CQRS.Core.Commands;

namespace SocialMedia.Command.WebApi.Commands
{
    public class UpdatePostCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}