using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;


namespace CrudTest.Controllers
{
    public class BookController : Controller
    {


        [HttpGet]
        public IActionResult InsertBook()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public string InsertBook(string name, string isbn, string publisher, string author, int quantity)
        {
            Book book = new Book()
            {
                Name = name,
                ISBN = isbn,
                Publisher = publisher,
                Author = author,
                Quantity = quantity
            };
            if (ModelState.IsValid)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Books.Add(book);
                    ctx.SaveChanges();
                }

                return "done";
            }

            else
                return "Failed";

        }

        public IActionResult ReadBooks()
        {

            var context = new ApplicationDbContext();
            var Books = context.Books.ToList();
            return View(Books);
        }

        public string DeleteBooks(int id)
        {

            using (var context = new ApplicationDbContext())
            {
                var book = context.Books.Find(id);
                context.Books.Remove(book);
                context.SaveChanges();
            }


            return "Done";
        }

        [HttpGet]
        public IActionResult UpdateBooks()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UpdateBooks(int id, string name, string isbn, string publisher, string author, int quantity)
        {


            using (var context = new ApplicationDbContext())
            {
                var book = context.Books.Find(id);

                book.Name = name;
                book.ISBN = isbn;
                book.Publisher = publisher;
                book.Author = author;
                book.Quantity = quantity;

                context.SaveChanges();
            }


            return "Done";
        }

    }
}
