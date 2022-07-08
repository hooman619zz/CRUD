using CrudTest.Models;

namespace CrudTest.Repository
{
    public interface IAuthorRepository
    {
        public void InsertAuthorOnPost(AuthorModel authorModel);

        public Task<AuthorModel> DeleteAuthorOnGet(int id);

        public List<AuthorModel> AuthorList();
        public void DeletAuthorOnPost(int id);
        public void Save();

    }
}
