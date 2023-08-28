using Microsoft.EntityFrameworkCore;
using SocialMedia.Query.Domain.Entities;

namespace SocialMedia.Query.Infrastructure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}