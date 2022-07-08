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
        public void InsertAuthorOnPost(AuthorModel authorModel)
        {
            _context.Authors.AddAsync(authorModel);
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
        public void DeletAuthorOnPost(int id)
        {
            var author = _context.Authors.Find(id);
            _context.Authors.Remove(author);
        }

        #endregion
        #region List
        public List<AuthorModel> AuthorList()
        {
            var authors =  _context.Authors.ToList();
            return authors;
        }
        #endregion
    }
}
