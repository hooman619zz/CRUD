using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudTest.Models
{
    [Table("Book", Schema = "Person")]
    public class AuthorModel : ISoftDelete
    {
        [Key]
        [Column("BookId")]
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The Name Most Be atleast 3 charaters")]
        [MaxLength(50, ErrorMessage = "The Name cannot Be more than atleast 20 charaters")]
        public string Name { get; set; }


        public ICollection<BookModel> BookModels { get; set; }

        public bool IsDeleted { get; set; }


    }
}
