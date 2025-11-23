using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Entities;

namespace Desktop.Repository
{
    public static class UserRepository
    {
        private static readonly List<UserModel> _users = new();

        public static UserModel? CurrentUser { get; private set; }

        // Регистрация
        public static bool Register(UserModel user, out string error)
        {
            error = string.Empty;

            // Email должен быть уникальным
            if (_users.Any(u => u.Email == user.Email))
            {
                error = "Пользователь с таким Email уже существует!";
                return false;
            }

            _users.Add(user);
            return true;
        }

        // Авторизация
        public static bool Login(string email, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password);

            if (user == null)
                return false;

            CurrentUser = user;
            return true;
        }
    }
}
