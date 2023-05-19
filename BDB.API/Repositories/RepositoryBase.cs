using BDB.API.Contracts;
using Microsoft.OpenApi.Any;

namespace BDB.API.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDb context;

        public RepositoryBase(AppDb context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll(bool trackChanges) =>
            trackChanges ?
            context.Set<T>() :
            context.Set<T>().AsNoTracking();

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ?
            context.Set<T>().Where(expression) :
            context.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) =>
            context.Set<T>().Add(entity);

        public void Update(T entity) =>
            context.Set<T>().Update(entity);

        public void Delete(T entity) =>
            context.Set<T>().Remove(entity);
    }
}
