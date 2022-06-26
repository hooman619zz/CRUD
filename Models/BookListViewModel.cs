using CrudTest.Models;

namespace CrudTest.Models
{
    public class BookListViewModel
    {
        public BookModel BookModel { get; set; }

        public List<AuthorModel> AuthorModel { get; set; }

        public List<LibraryModel> LibraryModel { get; set; }

    }
}
