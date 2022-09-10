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
        IUnitOfWork db;
        public BookController(ApplicationDbContext context)
        {
            db = new UnitOfWork(context);
        }
        #endregion


        #region Book

        #region Insert
        [Authorize(Trust = "yes")]
        [HttpGet]
        public async Task<IActionResult> InsertBook()
        {
            return View(await db.BookRepository.InsertBookOnGet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<RedirectResult> InsertBook(BookModel bookModel)
        {

            await db.BookRepository.InsertBookOnPost(bookModel);
            db.BookRepository.Save();


            return Redirect(@"~/Book/ReadBooks");


        }
        #endregion


        #region Read
        public IActionResult ReadBooks(int? id)
        {

            var books = db.BookRepository.ReadBooks(id);
            ViewBag.LibraryId = id;
            return View(books);
        }
        #endregion


        #region Delete
        [Authorize(Trust = "yes")]
        [HttpGet]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            return View(await db.BookRepository.DeleteBookOnGet(id));
        }

        [HttpPost]
        public async Task<RedirectResult> DeleteBooksOnPost(int id)
        {
            await db.BookRepository.DeleteBooksOnPost(id);
            db.BookRepository.Save();
            return Redirect(@"~/Book/ReadBooks");
        }

        #endregion


        #region Edit


        [Authorize(Trust = "yes")]
        [HttpGet]
        public IActionResult UpdateBookOnGet(int id)
        {
            return View(db.BookRepository.UpdateBookOnGet(id));
        }

        [HttpPost]
        public RedirectResult UpdateBookOnPost(BookModel bookModel)
        {


            if (bookModel != null)
            {
                db.BookRepository.UpdateBookOnPost(bookModel);
            }

            db.BookRepository.Save();


            return Redirect(@"~/Book/ReadBooks");

        }

        #endregion


        #region BookById


        [HttpGet]
        public async Task<IActionResult> BookById(int id)
        {
            var book = await db.BookRepository.GetBookById(id);
            return View(book);
        }
        #endregion

        #endregion

        [HttpPost]
        public JsonResult LoadData(string sortName, int? id, string sortDirection, int pageIndx, string availableItems)
        {
            Pagination<BookAuthorViewModel> model = new Pagination<BookAuthorViewModel>();
            var books = db.BookRepository.ReadBooks(id);
            int PageSize = 10;
            model.PageIndex = pageIndx;
            int startIndex = (model.PageIndex - 1) * PageSize;

            if (availableItems == "on")
                books.RemoveAll(b => b.books.Quantity == 0);

            model.LastPage = books.Count() / 10 + 1;
            model.Data = OrderBySubject(sortName, sortDirection, startIndex, PageSize, books);

            string json = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            return new JsonResult(json);
        }

        public List<BookAuthorViewModel> OrderBySubject(string sortName, string sortDirection, int startIndex, int PageSize, List<BookAuthorViewModel> books)
        {
            List<BookAuthorViewModel> Books = new List<BookAuthorViewModel>();
            switch (sortName)
            {
                case "BookID":
                case "":
                    if (sortDirection == "ASC")
                        Books = books.OrderBy(b => b.books.Id).Skip(startIndex)
                            .Take(PageSize).ToList();
                    else
                        Books = books.OrderByDescending(b => b.books.Id).Skip(startIndex)
                            .Take(PageSize).ToList();
                    break;
                case "Name":
                    if (sortDirection == "ASC")
                        Books = books.OrderBy(b => b.books.Name).Skip(startIndex)
                            .Take(PageSize).ToList();
                    else
                        Books = books.OrderByDescending(b => b.books.Name).Skip(startIndex)
                            .Take(PageSize).ToList();
                    break;
                case "ISBN":
                    if (sortDirection == "ASC")
                        Books = books.OrderBy(b => b.books.ISBN).Skip(startIndex)
                            .Take(PageSize).ToList();
                    else
                        Books = books.OrderByDescending(b => b.books.ISBN).Skip(startIndex)
                            .Take(PageSize).ToList();
                    break;
                case "Publisher":
                    if (sortDirection == "ASC")
                        Books = books.OrderBy(b => b.books.Publisher).Skip(startIndex)
                            .Take(PageSize).ToList();
                    else
                        Books = books.OrderByDescending(b => b.books.Publisher).Skip(startIndex)
                            .Take(PageSize).ToList();
                    break;
                case "Rate":
                    if (sortDirection == "ASC")
                        Books = books.OrderBy(b => b.books.Rate).Skip(startIndex)
                            .Take(PageSize).ToList();
                    else
                        Books = books.OrderByDescending(b => b.books.Rate).Skip(startIndex)
                            .Take(PageSize).ToList();
                    break;
                case "Price":
                    if (sortDirection == "ASC")
                        Books = books.OrderBy(b => b.books.Price).Skip(startIndex)
                            .Take(PageSize).ToList();
                    else
                        Books = books.OrderByDescending(b => b.books.Price).Skip(startIndex)
                            .Take(PageSize).ToList();
                    break;
                case "AuthorName":
                    Books = books.OrderBy(b => b.author.Name).Skip(startIndex)
                            .Take(PageSize).ToList();
                    break;

            }
            return Books;
        }

        public IActionResult GetBooks(int? id)
        {

            var books = db.BookRepository.ReadBooks(id);
            return View(books);
        }

    }
}
