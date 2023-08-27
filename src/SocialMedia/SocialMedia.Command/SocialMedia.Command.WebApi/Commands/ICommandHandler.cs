namespace SocialMedia.Command.WebApi.Commands
{
    public interface ICommandHandler
    {
        public Task HandleAsync(NewPostCommand command);
        public Task HandleAsync(UpdatePostCommand command);
        public Task HandleAsync(DeletePostCommand command);
        public Task HandleAsync(LikePostCommand command);
        public Task HandleAsync(AddCommentCommand command);
        public Task HandleAsync(UpdateCommentCommand command);
        public Task HandleAsync(RemoveCommentCommand command);
    }
}