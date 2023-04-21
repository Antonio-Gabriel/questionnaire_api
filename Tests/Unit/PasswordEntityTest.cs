using NUnit.Framework;
using QuestionaryApp.Application.Security.Bcrypt;

namespace Tests.Unit
{
    public class PasswordEntityTest
    {
        [Test]
        public void NeedToReturnTrue()
        {
            string passEncryptedHash = Bcrypt.Encrypt("antoniocampos");
            bool isValid = Bcrypt.Verify("antoniocampos", passEncryptedHash);

            Assert.That(isValid, Is.True);
        }
    }
}