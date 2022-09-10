using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;
namespace CrudTest.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        private BookRepository bookRepository;

        public BookRepository BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(context);
                }

                return bookRepository;
            }
        }

        private AuthorRepository authorRepository;

        public AuthorRepository AuthorRepository
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new AuthorRepository(context);
                }

                return authorRepository;
            }
        }

        private LibraryRepository libraryRepository;

        public LibraryRepository LibraryRepository
        {
            get
            {
                if (libraryRepository == null)
                {
                    libraryRepository = new LibraryRepository(context);
                }

                return libraryRepository;
            }
        }
    }
}
