using System.Text.RegularExpressions;

namespace Desktop
{
    public static class InputValidator
    {
        /// <summary>
        /// Проверка имени — не менее 3 символов.
        /// </summary>
        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Trim().Length >= 3;
        }

        /// <summary>
        /// Проверка почты по шаблону *@*.* (простая, но надёжная).
        /// </summary>
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Проверка пароля — минимум 6 символов.
        /// </summary>
        public static bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }
    }
}