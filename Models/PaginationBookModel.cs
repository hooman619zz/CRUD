using CrudTest.Models;

namespace CrudTest.Controllers
{
    public class PaginationBookModel
    {
        public List<BookAuthorViewModel> Books { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int LastPage { get; set; }


    }
}
