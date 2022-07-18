using CrudTest.Models;

namespace CrudTest.Repository
{
    public interface ILibraryRepository
    {
        public Task<List<BookModel>> InsertLibraryOnGet();

        public void InsertLibraryOnPost(LibraryModel libraryModel, int[] arrays);

        public void Save();

        public Task<List<LibraryModel>> LibraryList();

        public LibraryModel DeleteLibraryOnGet(int id);
        public void DeleteLibraryOnPost(int id);

        public LibraryModel UpdateLibraryOnGet(int id);

        public void UpdateLibraryOnPost(LibraryModel libraryModel);
    }
}
