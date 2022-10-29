using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;
namespace CrudTest.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; }

        public IAuthorRepository AuthorRepository { get; }

        public ILibraryRepository LibraryRepository { get; }
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context,IBookRepository bookRepository,ILibraryRepository libraryRepository,IAuthorRepository authorRepository)
        {
            this.context = context;
            this.BookRepository = bookRepository;
            this.AuthorRepository = authorRepository;
            this.LibraryRepository = libraryRepository;
        }


    }
}
