using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using System;

namespace EnFlights.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUsernameAndPasswordCorrect(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUsernameUnique(string username)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
