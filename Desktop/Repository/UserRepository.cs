using Todo.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Desktop.Repository
{
    public static class UserRepository
    {
        private static readonly List<UserModel> _users = new();

        public static bool Register(UserModel newUser, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (_users.Any(u => u.Email == newUser.Email))
            {
                errorMessage = "Пользователь с такой почтой уже существует.";
                return false;
            }

            _users.Add(newUser);
            return true;
        }

        public static UserModel? Login(string email, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            var user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                errorMessage = "Неверный email или пароль.";
                return null;
            }

            return user;
        }
    }
}
