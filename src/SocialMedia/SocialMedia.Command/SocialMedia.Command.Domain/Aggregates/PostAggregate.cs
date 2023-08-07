using CQRS.Core.Domain;
using SocialMedia.Common.Events;

namespace SocialMedia.Command.Domain.Aggregates
{
    public class PostAggregate : AggregateRoot
    {
        private bool _active;
        private string _author;
        private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

        public bool Active
        {
            get => _active; set => _active = value;
        }

        public PostAggregate()
        { }

        public PostAggregate(Guid id, string author, string message)
        {
            RaiseEvent(new PostCreatedEvent()
            {
                Id = id,
                Author = author,
                Message = message,
                CreatedAt = DateTime.Now
            });
        }

        public void Apply(PostCreatedEvent @event)
        {
            _id = @event.Id;
            _author = @event.Author;
            _active = true;
        }

        public void UpdatePost(string message)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit the message of inactive post!");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException($"The value of {nameof(message)} cannot be null. Verify and try again!");
            }

            RaiseEvent(new PostUpdatedEvent()
            {
                Id = _id,
                Message = message,
                UpdatedAt = DateTime.Now
            });
        }

        public void Apply(PostUpdatedEvent @event)
        {
            _id = @event.Id;
        }

        public void LikePost()
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot like an inactive post!");
            }

            RaiseEvent(new PostLikedEvent()
            {
                Id = _id,
                CreatedAt = DateTime.Now
            });
        }

        public void Apply(PostLikedEvent @event)
        {
            _id = @event.Id;
        }

        public void DeletePost(string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot comment an inactive post!");
            }

            if (_author.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You not allowed to delete the post of another user!");
            }

            RaiseEvent(new PostDeletedEvent()
            {
                Id = _id,
                DeletedAt = DateTime.Now
            });
        }

        public void Apply(PostDeletedEvent @event)
        {
            _id = @event.Id;
            _active = false;
        }

        public void AddComment(string comment, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot comment an inactive post!");
            }

            if (string.IsNullOrEmpty(comment))
            {
                throw new InvalidOperationException($"The value of your comment cannot be null. Verify and try again!");
            }

            RaiseEvent(new CommentAddedEvent()
            {
                Id = _id,
                CommentId = Guid.NewGuid(),
                Comment = comment,
                Username = username,
                CreatedAt = DateTime.Now
            });
        }

        public void Apply(CommentAddedEvent @event)
        {
            _id = @event.Id;
            _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.Username));
        }

        public void UpdateComment(Guid commentId, string username, string comment)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit a comment of an inactive post!");
            }

            if (string.IsNullOrEmpty(comment))
            {
                throw new InvalidOperationException($"The value of {nameof(comment)} cannot be null. Verify and try again!");
            }

            if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You not allowed to edit the comment of another user!");
            }

            RaiseEvent(new CommentUpdatedEvent()
            {
                Id = _id,
                CommentId = commentId,
                Username = username,
                Comment = comment,
                UpdatedAt = DateTime.Now
            });
        }

        public void Apply(CommentUpdatedEvent @event)
        {
            _id = @event.Id;
            _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.Username);
        }

        public void RemoveComment(Guid commentId, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot remove a comment of an inactive post!");
            }

            if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You not allowed to remove the comment of another user!");
            }

            RaiseEvent(new CommentRemovedEvent()
            {
                Id = _id,
                CommentId = commentId,
                Username = username,
                RemovedAt = DateTime.Now
            });
        }

        public void Apply(CommentRemovedEvent @event)
        {
            _id = @event.Id;
            _comments.Remove(@event.CommentId);
        }
    }
}