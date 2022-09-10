using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;


namespace CrudTest.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }


        #region Insert Author
        public async Task InsertAuthorOnPost(AuthorModel authorModel)
        {
           await _context.Authors.AddAsync(authorModel);
        }
        #endregion
        #region Save
        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion
        #region Delete Author
        public async Task<AuthorModel> DeleteAuthorOnGet(int id)
        {
            var author = await _context.Authors
                    .Where(a => a.Id == id)
                     .Select(s => new AuthorModel()
                     {
                         Id = s.Id,
                         Name = s.Name
                     }).FirstOrDefaultAsync();
            return author;
        }
        public async Task DeletAuthorOnPost(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
        }

        #endregion
        #region List
        public async Task<List<AuthorModel>> AuthorList()
        {
            var authors = await  _context.Authors.ToListAsync();
            return authors;
        }
        #endregion
    }
}
