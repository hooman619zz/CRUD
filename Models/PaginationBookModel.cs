using CrudTest.Models;

namespace CrudTest.Controllers
{
    public class PaginationBookModel
    {
        public List<BookModel> bookModel { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }
}
