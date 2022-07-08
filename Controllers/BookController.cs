﻿using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;

namespace CrudTest.Controllers
{
    public class BookController : Controller
    {
        #region ctor + jections
        //private ApplicationDbContext _context;

        private BookRepository bookRepository;
        private AuthorRepository authorRepository;
        private LibraryRepository libraryRepository;
        public BookController()
        {
            //_context = context;
            bookRepository = new BookRepository(new ApplicationDbContext());
            authorRepository = new AuthorRepository(new ApplicationDbContext());
            libraryRepository = new LibraryRepository(new ApplicationDbContext());
        }

        #endregion


        #region Book


        #region Insert
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


            return  Redirect(@"~/Book/ReadBooks");


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

        [HttpGet]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            return View(await bookRepository.DeleteBookOnGet(id));
        }

        [HttpPost]
        public async Task<RedirectResult> DeleteBooksOnPost(int id)
        {
            await Task.Run(()=> bookRepository.DeleteBooksOnPost(id));
            bookRepository.Save();
            return Redirect(@"~/Book/ReadBooks");
        }

        #endregion


        #region Edit



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


        #endregion


    }
}
