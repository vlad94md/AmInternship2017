﻿using EnFlights.ApplicationCore.Entities;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface IUserService
    {
        bool IsUsernameAndPasswordCorrect(string username, string password);
        bool IsUsernameUnique(string username);
        void RegisterUser(User user);
    }
}
