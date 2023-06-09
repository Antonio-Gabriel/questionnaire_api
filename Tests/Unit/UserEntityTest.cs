using NUnit.Framework;

namespace Tests.Unit
{
    public class UserEntityTest
    {
        User? _user;

        [SetUp]
        public void Setup()
        {
            _user = new User
            {
                Name = "Antonio Gabriel",
                Email = "antoniocamposgabriel@gmail.com",
                CodeName = "AgDevCoder",
            };
        }

        [Test]
        public void TestIfTheEntityReturnsAnUuid()
        {
            Assert.That(_user!.Id, Is.Not.Null);
            Assert.IsInstanceOf<Guid>(_user.Id);
            Assert.AreEqual("antoniocamposgabriel@gmail.com", _user.Email);
        }

        [Test]
        public void TestEmailValidationOnEntity()
        {
            Assert.IsTrue(EmailValidator.IsValid(_user!.Email));
        }
    }
}