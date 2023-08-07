using CQRS.Core.Commands;

namespace SocialMedia.Command.WebApi.Commands
{
    public class DeletePostCommand : BaseCommand
    {
        public string Username { get; set; }
    }
}