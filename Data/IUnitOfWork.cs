using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;

namespace CrudTest.Data
{
    public interface IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public ILibraryRepository LibraryRepository { get; }

    }
}
