using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CrudTest.Models
{
    public delegate void QuantityHandler(BookModel book, int Quantity);


    [Table("Book", Schema = "Book")]
    public class BookModel :BaseEntity , ISoftDelete
    {

        public event QuantityHandler quantityHandler;





        public string Name { get; set; }



        public string ISBN { get; set; }



        public string Publisher { get; set; }

        #region Navigation props
        public int AuthorId { get; set; }

        public AuthorModel AuthorModel { get; set; }


        public virtual List<LibraryModel> LibraryModels { get; set; }

        #endregion

        public bool IsDeleted { get; set; }

        public int Rate { get; set; }

        public int Price { get; set; }

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
