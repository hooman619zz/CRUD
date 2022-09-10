using CrudTest.Models;

namespace CrudTest.Repository
{
    public interface IAuthorRepository
    {
        public Task InsertAuthorOnPost(AuthorModel authorModel);

        public Task<AuthorModel> DeleteAuthorOnGet(int id);

        public Task<List<AuthorModel>> AuthorList();
        public Task DeletAuthorOnPost(int id);
        public void Save();

    }
}
