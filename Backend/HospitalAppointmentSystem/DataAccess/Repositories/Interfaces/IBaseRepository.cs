using System.Linq.Expressions;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id); // ID'ye göre tek bir entity getir
        Task<IEnumerable<TEntity>> GetAllAsync(); // Tüm entity'leri getir
        Task<List<TEntity>> GetAllByIdAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate); // Şarta göre getir
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity); // Yeni entity ekle
        Task AddRangeAsync(IEnumerable<TEntity> entities); // Birden fazla entity ekle

        void Remove(TEntity entity); 
        void RemoveRange(IEnumerable<TEntity> entities); 

        void Update(TEntity entity);
        Task<int> CountAllAsync();
    }
}
