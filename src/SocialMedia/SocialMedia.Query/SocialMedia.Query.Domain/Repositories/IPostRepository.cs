using SocialMedia.Query.Domain.Entities;

namespace SocialMedia.Query.Domain.Repositories
{
    public interface IPostRepository : IGenericRepository<PostEntity>
    {
        public Task<PostEntity> GetPostByIdAsync(Guid postId);
        public Task<ICollection<PostEntity>> GetAllPostsAsync();
        public Task<ICollection<PostEntity>> GetPostsWithLikesAsync(int numberOfLikes);
        public Task<ICollection<PostEntity>> GetPostsByAuthorAsync(string authorName);
        public Task<ICollection<PostEntity>> GetPostsWithCommentsAsync();
    }
}