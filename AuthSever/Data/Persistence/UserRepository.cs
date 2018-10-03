using AuthSever.Data.Models;
using System.Linq;


namespace AuthSever.Data.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IUsersDbContext _context;

        public UserRepository(IUsersDbContext context) {
            _context = context;
        }

        public User Register(User user) {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Login(string userId, string password) {
            var _user = _context.Users.FirstOrDefault(u => u.UserId==userId && u.Password==password);
            return _user;
        }

        public User FindUserById(string userId) {
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId);
            return user;
        }

    }
}
