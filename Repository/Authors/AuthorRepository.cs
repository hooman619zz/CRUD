using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Repository
{
    public class AuthorRepository :GenericRepository<AuthorModel>, IAuthorRepository
    {
        private ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext applicationDbContext) :base(applicationDbContext)
        {
            this._context = applicationDbContext;
        }



        #region Save
        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

    }
}
