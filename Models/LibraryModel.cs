using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudTest.Models
{
    public class LibraryModel: BaseEntity
    {




        public string Name { get; set; }



        public string Address { get; set; }

        public virtual List<BookModel> BookModels { get; set; }

        public List<BookModel> bookModels;

    }
}
