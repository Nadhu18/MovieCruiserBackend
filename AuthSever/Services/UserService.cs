using AuthSever.Data.Models;
using AuthSever.Data.Persistence;
using AuthSever.Exceptions;

namespace AuthSever.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) {
            _repo = repo;
        }

        public bool IsUserExists(string UserId) {
            var user = _repo.FindUserById(UserId);
            if (user != null) {
                return true;
            }
            return false;
        }

        public User Login(string UserId, string Password) {
            var user = _repo.Login(UserId, Password);
            if (user != null)
            {
                return user;
            }
            else {
                throw new UserNotFoundException("Invalid userId or password");
            }
        }

        public User Register(User UserDetails) {
            var user = _repo.Register(UserDetails);
            return user;
        }
    }
}
