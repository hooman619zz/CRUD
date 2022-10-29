using CrudTest.Models;

namespace CrudTest.Repository
{
    public interface IAuthorRepository:IGenericRepository<AuthorModel>
    {
        public void Save();

    }
}
