using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Query.Domain.Repositories;
using SocialMedia.Query.Infrastructure.DataAccess;
using SocialMedia.Query.Infrastructure.Handlers;
using SocialMedia.Query.Infrastructure.Repositories;

using EventHandler = SocialMedia.Query.Infrastructure.Handlers.EventHandler;

namespace SocialMedia.Query.Infrastructure
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Database Context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Create Database and Tables from code
            ApplicationDbContext dataContext = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            dataContext.Database.EnsureCreated();

            // Dependency Injection
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IEventHandler, EventHandler>();

            return services;
        }
    }
}