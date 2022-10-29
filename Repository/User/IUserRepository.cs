using CrudTest.Models;

namespace CrudTest.Repository
{
    public interface IUserRepository
    {
        public List<UserModel> UserList();
        public void InsertUserOnPost(UserModel userModel);
        public void Save();
    }
}
