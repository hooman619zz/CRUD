using CrudTest.Models;

namespace CrudTest.Repository
{
    public interface ILibraryRepository:IGenericRepository<LibraryModel>
    {
        public Task<List<BookModel>> InsertLibraryOnGet();

        public Task InsertLibraryOnPost(LibraryModel libraryModel, int[] arrays);

        public void Save();


        public LibraryModel DeleteLibraryOnGet(int id);

        public LibraryBooksViewModel UpdateLibraryOnGet(int id);

        public void UpdateLibraryOnPost(LibraryModel libraryModel, int[] arrays);
    }
}
