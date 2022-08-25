using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CrudTest.Controllers
{
    public class BookController : Controller
    {
        #region ctor + jections

        private IBookRepository bookRepository;

        public BookController(ApplicationDbContext context)
        {
            this.bookRepository = new BookRepository(context);
        }

        #endregion


        #region Book

        #region Insert
        [Authorize(Trust = "yes")]
        [HttpGet]
        public async Task<IActionResult> InsertBook()
        {
            return View(await bookRepository.InsertBookOnGet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public RedirectResult InsertBook(BookModel bookModel)
        {

            bookRepository.InsertBookOnPost(bookModel);
            bookRepository.Save();


            return Redirect(@"~/Book/ReadBooks");


        }
        #endregion


        #region Read
        public IActionResult ReadBooks(int? id)
        {

            var books = bookRepository.ReadBooks(id);
            return View(books);
        }
        #endregion


        #region Delete
        [Authorize(Trust = "yes")]
        [HttpGet]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            return View(await bookRepository.DeleteBookOnGet(id));
        }

        [HttpPost]
        public async Task<RedirectResult> DeleteBooksOnPost(int id)
        {
            await Task.Run(() => bookRepository.DeleteBooksOnPost(id));
            bookRepository.Save();
            return Redirect(@"~/Book/ReadBooks");
        }

        #endregion


        #region Edit


        [Authorize(Trust = "yes")]
        [HttpGet]
        public IActionResult UpdateBookOnGet(int id)
        {
            return View(bookRepository.UpdateBookOnGet(id));
        }

        [HttpPost]
        public RedirectResult UpdateBookOnPost(BookModel bookModel)
        {


            if (bookModel != null)
            {
                bookRepository.UpdateBookOnPost(bookModel);
            }

            bookRepository.Save();


            return Redirect(@"~/Book/ReadBooks");

        }

        #endregion

        #region BookById


        [HttpGet]
        public async Task<IActionResult> BookById(int id)
        {
            var book = await bookRepository.GetBookById(id);
            return View(book);
        }
        #endregion

        #endregion

        [HttpPost]
        public JsonResult AjaxMethod(string sortName,int? id,string sortDirection)
        {
            var books = bookRepository.ReadBooks(id);
            List<BookAuthorViewModel> model = new List<BookAuthorViewModel>();
            switch (sortName)
            {
                case "BookID":
                case "":
                    if (sortDirection == "ASC")
                        model = books.OrderBy(b => b.books.Id).ToList();
                    else
                        model = books.OrderByDescending(b => b.books.Id).ToList();
                    break;
                case "Name":
                    if (sortDirection == "ASC")
                        model = books.OrderBy(b => b.books.Name).ToList();
                    else
                        model = books.OrderByDescending(b => b.books.Name).ToList();
                    break;
                case "ISBN":
                    if (sortDirection == "ASC")
                        model = books.OrderBy(b => b.books.ISBN).ToList();
                    else
                        model = books.OrderByDescending(b => b.books.ISBN).ToList();
                    break;
                case "Publisher":
                    if (sortDirection == "ASC")
                        model = books.OrderBy(b => b.books.Publisher).ToList();
                    else
                        model = books.OrderByDescending(b => b.books.Publisher).ToList();
                    break;
                case "Rate":
                    if (sortDirection == "ASC")
                        model = books.OrderBy(b => b.books.Rate).ToList();
                    else
                        model = books.OrderByDescending(b => b.books.Rate).ToList();
                    break;
                case "Price":
                    if (sortDirection == "ASC")
                        model = books.OrderBy(b => b.books.Price).ToList();
                    else
                        model = books.OrderByDescending(b => b.books.Price).ToList();
                    break;
                case "AuthorName":
                    model = books.OrderBy(b => b.author.Name).ToList();
                    break;

            }
            //string jsonString = JsonSerializer.Serialize(model);
            string json = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return new JsonResult(json);
        }

        public IActionResult GetBooks(int? id)
        {

            var books = bookRepository.ReadBooks(id);
            return View(books);
        }
        [HttpPost]
        public JsonResult LoadData()
        {
            System.Threading.Thread.Sleep(20000);
            IEnumerable<BookModel> Books = bookRepository.GetBooks();
            return Json(new { data = Books, recordsFiltered= Books.Count(),recordsTotal= Books.Count()});
        }
    }
}
