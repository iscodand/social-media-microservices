using Microsoft.EntityFrameworkCore;
using SocialMedia.Query.Domain.Entities;
using SocialMedia.Query.Domain.Repositories;
using SocialMedia.Query.Infrastructure.DataAccess;

namespace SocialMedia.Query.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<PostEntity>, IPostRepository
    {
        private readonly DbSet<PostEntity> _posts;

        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _posts = context.Posts;
        }

        public async Task<ICollection<PostEntity>> GetAllPostsAsync()
        {
            return await _posts.AsNoTracking()
                                           .Include(x => x.Comments).AsNoTracking()
                                           .ToListAsync();
        }

        public async Task<PostEntity> GetPostByIdAsync(Guid postId)
        {
            return await _posts.Include(x => x.Comments)
                                           .FirstOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<ICollection<PostEntity>> GetPostsByAuthorAsync(string authorName)
        {
            return await _posts.AsNoTracking()
                                           .Include(x => x.Comments).AsNoTracking()
                                           .Where(x => x.Author == authorName)
                                           .ToListAsync();
        }

        public async Task<ICollection<PostEntity>> GetPostsWithCommentsAsync()
        {
            return await _posts.AsNoTracking()
                                           .Include(x => x.Comments).AsNoTracking()
                                           .Where(x => x.Comments.Any() && x.Comments != null)
                                           .ToListAsync();
        }

        public async Task<ICollection<PostEntity>> GetPostsWithLikesAsync(int numberOfLikes)
        {
            return await _posts.AsNoTracking()
                                           .Include(x => x.Comments).AsNoTracking()
                                           .Where(x => x.Likes == numberOfLikes)
                                           .ToListAsync();
        }
    }
}