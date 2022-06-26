using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudTest.Models
{
    public class LibraryModel
    {
        [Key]
        [Column("LibraryId")]
        public int Id { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "The Name Most Be atleast 3 charaters")]
        [MaxLength(50, ErrorMessage = "The Name cannot Be more than atleast 20 charaters")]
        public string Name { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "The Name Most Be atleast 3 charaters")]
        public string Address { get; set; }

        public virtual ICollection<BookModel> BookModels { get; set; }

        public List<BookModel> bookModels;

    }
}
