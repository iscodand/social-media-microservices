using SocialMedia.Query.Domain.Entities;
using SocialMedia.Query.Domain.Repositories;
using SocialMedia.Query.Infrastructure.DataAccess;

namespace SocialMedia.Query.Infrastructure.Repositories
{
    public class CommentsRepository : GenericRepository<CommentEntity>, ICommentsRepository
    {
        public CommentsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}