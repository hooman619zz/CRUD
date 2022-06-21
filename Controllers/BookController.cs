using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;


namespace CrudTest.Controllers
{
    public class BookController : Controller
    {

        #region Insert
        [HttpGet]
        public IActionResult InsertBook()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public RedirectResult InsertBook(string name, string isbn, string publisher, string author, int quantity)
        {
            Book book = new Book()
            {
                Name = name,
                ISBN = isbn,
                Publisher = publisher,
                Author = author,
                Quantity = quantity
            };
            //if (ModelState.IsValid)
            //{
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(book);
                ctx.SaveChanges();
            }

            return Redirect(@"~/Book/InsertBook");
            //}


        }
        #endregion

        #region Read
        public IActionResult ReadBooks()
        {

            var context = new ApplicationDbContext();
            var Books = context.Books.ToList();
            return View(Books);
        }
        #endregion

        #region Delete

        [HttpPost]
        public RedirectResult DeleteBooks(int id)
        {

            using (var context = new ApplicationDbContext())
            {
                var book = context.Books.Find(id);
                context.Books.Remove(book);
                context.SaveChanges();
            }


            return Redirect(@"~/Book/ReadBooks");
        }
        #endregion

         
        #region Edit



        [HttpGet]
        public IActionResult OnGet(int id)
        {

            using (var context = new ApplicationDbContext())
            {

                var book = context.Books
                    .Where(b => b.Id == id)
                     .Select(s => new Book()
                     {

                         Id = s.Id,
                         Name = s.Name,
                         Author = s.Author,
                         Publisher = s.Publisher,
                         ISBN = s.ISBN,
                         Quantity = s.Quantity,


                     }).FirstOrDefault();
                return View(book);

            }


        }

        [HttpPost]
        public RedirectResult OnPost(int id, string name, string isbn, string publisher, string author, int quantity)
        {

            using (var context = new ApplicationDbContext())
            {
                var book = context.Books.Find(id);
                if (book != null)
                {
                    book.Name = name;
                    book.ISBN = isbn;
                    book.Publisher = publisher;
                    book.Author = author;
                    book.Quantity = quantity;
                }

                context.SaveChanges();

            }
            return Redirect(@"~/Book/ReadBooks");

        }

        #endregion
    }
}
