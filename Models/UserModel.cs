using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudTest.Models
{
    [Table("Users", Schema = "Person")]
    public class UserModel:BaseEntity
    {



        public string UserName { get; set; }


        public string Password { get; set; }

    }
}
