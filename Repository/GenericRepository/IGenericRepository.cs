namespace CrudTest.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Update(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task<List<T>> GetAllAsync();
    }
}
