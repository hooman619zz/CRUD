using CrudTest.Models;

namespace CrudTest.Repository
{
    public interface ILibraryRepository
    {
        public Task<List<BookModel>> InsertLibraryOnGet();

        public Task InsertLibraryOnPost(LibraryModel libraryModel, int[] arrays);

        public void Save();

        public Task<List<LibraryModel>> LibraryList();

        public LibraryModel DeleteLibraryOnGet(int id);
        public Task DeleteLibraryOnPost(int id);

        public LibraryBooksViewModel UpdateLibraryOnGet(int id);

        public void UpdateLibraryOnPost(LibraryModel libraryModel, int[] arrays);
    }
}
