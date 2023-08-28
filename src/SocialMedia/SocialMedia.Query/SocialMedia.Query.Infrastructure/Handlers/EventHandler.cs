using SocialMedia.Common.Events;
using SocialMedia.Query.Domain.Entities;
using SocialMedia.Query.Domain.Repositories;

namespace SocialMedia.Query.Infrastructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public EventHandler(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task On(PostCreatedEvent @event)
        {
            PostEntity post = new()
            {
                Id = @event.Id,
                Author = @event.Author,
                Message = @event.Message,
                CreatedAt = @event.CreatedAt,
                UpdatedAt = @event.CreatedAt
            };

            await _postRepository.CreateAsync(post);
        }

        public async Task On(PostUpdatedEvent @event)
        {
            PostEntity post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null)
            {
                return;
            }

            post.Message = @event.Message;
            post.Edited = true;
            post.UpdatedAt = @event.UpdatedAt;

            await _postRepository.UpdateAsync(post);
        }

        public async Task On(PostLikedEvent @event)
        {
            PostEntity post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null)
            {
                return;
            }

            post.Likes++;

            await _postRepository.UpdateAsync(post);
        }

        public async Task On(PostDeletedEvent @event)
        {
            PostEntity post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null)
            {
                return;
            }

            await _postRepository.DeleteAsync(post);
        }

        public async Task On(CommentAddedEvent @event)
        {
            CommentEntity comment = new()
            {
                Id = @event.CommentId,
                PostId = @event.Id,
                Comment = @event.Comment,
                Username = @event.Username,
                CreatedAt = @event.CreatedAt,
                UpdatedAt = @event.CreatedAt
            };

            await _commentRepository.CreateAsync(comment);
        }

        public async Task On(CommentUpdatedEvent @event)
        {
            CommentEntity comment = await _commentRepository.GetByIdAsync(@event.CommentId);

            if (comment == null)
            {
                return;
            }

            comment.Comment = @event.Comment;
            comment.Edited = true;
            comment.UpdatedAt = @event.UpdatedAt;

            await _commentRepository.UpdateAsync(comment);
        }

        public async Task On(CommentRemovedEvent @event)
        {
            CommentEntity comment = await _commentRepository.GetByIdAsync(@event.CommentId);

            if (comment == null)
            {
                return;
            }

            await _commentRepository.DeleteAsync(comment);
        }
    }
}