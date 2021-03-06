﻿using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Helpers;
using EnFlights.ApplicationCore.Interfaces;
using System.IO;
using System.Linq;

namespace EnFlights.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUsernameAndPasswordCorrect(string username, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == username && u.Password == password.GetSHA256Hash());

            return user != null;
        }

        public bool IsUsernameUnique(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == username);

            return user == null;
        }

        public User RegisterUser(User user)
        {
            if(user == null) throw new InvalidDataException("User data is invalid");

            var hashedPassword = user.Password.GetSHA256Hash();
            user.Password = hashedPassword;

            return _userRepository.Add(user);
        }
    }
}
