using CrudTest.Models;

namespace CrudTest.Controllers
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int LastPage { get; set; }


    }
}
