namespace QuestionaryApp.Application.Security.Bcrypt
{
    public struct Bcrypt
    {
        private static int _workFactor = 14;

        public static string Encrypt(string text)
            => BCrypt.Net.BCrypt.HashPassword(text, _workFactor);

        public static bool Verify(string text, string hash)
            => BCrypt.Net.BCrypt.Verify(text, hash);
    }
}