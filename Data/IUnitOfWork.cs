using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;

namespace CrudTest.Data
{
    public interface IUnitOfWork
    {
        public BookRepository BookRepository { get; }
        public AuthorRepository AuthorRepository { get; }
        public LibraryRepository LibraryRepository { get; }

    }
}
