using P010Store.Entities;
using System.Linq.Expressions;

namespace P010Store.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T,bool>> expression);
        T Get(Expression<Func<T, bool>> expression);

        T Find(int id);

        int Add(T entity);

        void Update(T entity);
        void Delete(T entity);

        int SaveChanges();

        //Async Versions

        Task<T> FindAsync(int id);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> FindAllAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
