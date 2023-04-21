using System.Text.RegularExpressions;

namespace QuestionaryApp.Domain.Validations
{
    public abstract class EmailValidator
    {
        public static bool IsValid(string email)
        {
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^([a-z0-9\+_\-]+)(\.[a-z0-9\+_\-]+)*@([a-z0-9\-]+\.)+[a-z]{2,6}$"))
            {
                return false;
            }

            return true;
        }
    }
}