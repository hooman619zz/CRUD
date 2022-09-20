using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private ApplicationDbContext context;
        private DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task InsertAsync(T entity)
        {
             await dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);

        }

        public virtual async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
