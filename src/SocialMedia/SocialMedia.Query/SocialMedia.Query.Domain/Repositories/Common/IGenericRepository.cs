using SocialMedia.Query.Domain.Entities;

namespace SocialMedia.Query.Domain.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<ICollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(Guid id);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}