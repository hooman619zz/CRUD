using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CrudTest.Models
{
    public delegate void QuantityHandler(BookModel book, int Quantity);


    [Table("Book", Schema = "Book")]
    public class BookModel
    {

        public event QuantityHandler quantityHandler;
        // [Required]

        [Key]
        [Column("BookId")]
        public int Id { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "The Name Most Be atleast 3 charaters")]
        [MaxLength(50, ErrorMessage = "The Name cannot Be more than atleast 20 charaters")]
        public string Name { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "The ISBN Most Be atleast 3 charaters")]
        [MaxLength(50, ErrorMessage = "The ISBN cannot Be more than atleast 20 charaters")]
        public string ISBN { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "The Publisher Most Be atleast 3 charaters")]
        [MaxLength(50, ErrorMessage = "The Publisher cannot Be more than atleast 20 charaters")]
        public string Publisher { get; set; }

        #region Navigation props
        [ForeignKey("AuthorModel")]
        public int AuthorId { get; set; }

        public AuthorModel AuthorModel { get; set; }


        public virtual ICollection<LibraryModel> LibraryModels { get; set; }

        #endregion



        private int quantity;


        [Required]
        [Range(0, 99999)]
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0)
                {
                    if (quantityHandler == null)
                        throw new ArgumentOutOfRangeException("Quantity", value, "Quantity<0");
                    quantityHandler(this, value);
                }
                else
                {
                    quantity = value;
                }

            }
        }



    }
}
