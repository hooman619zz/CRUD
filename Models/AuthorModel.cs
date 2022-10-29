using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudTest.Models
{
    [Table("Author", Schema = "Person")]
    public class AuthorModel :BaseEntity , ISoftDelete
    {



        public string Name { get; set; }


        public ICollection<BookModel> BookModels { get; set; }

        public bool IsDeleted { get; set; }


    }
}
