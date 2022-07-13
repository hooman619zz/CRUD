using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Repository
{
    public class UserRepository : IUserRepository
    {

        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }


        #region Add User
        public void InsertUserOnPost(UserModel userModel)
        {
            _context.Users.AddAsync(userModel);
        }
        #endregion

        #region save
        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion

        #region List
        public List<UserModel> UserList()
        {
            var users = _context.Users.ToList();
            return users;
        }
        #endregion
    }
}
