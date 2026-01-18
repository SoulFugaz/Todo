using System.Text.RegularExpressions;

namespace Desktop
{
    public static class InputValidator
    {
        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Trim().Length >= 2;
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }
    }
}
