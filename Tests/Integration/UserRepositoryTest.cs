using NUnit.Framework;

namespace Tests.Integration
{
    public class UserRepositoryTest
    {
        private UserRepository _userRepository;
        public UserRepositoryTest()
        {
            var _context = DataContextFactory.create();
            _userRepository = new UserRepository(_context);
        }

        [Test]
        public async Task ReturnUserLists()
        {
            var users = await _userRepository.GetAll();
            Assert.IsNotNull(users);
        }

        [Test]
        public async Task ReturnTrueAfterInsertUserFromDb()
        {
            var user = new User
            {
                Name = "Kiala Gabrielaaa",
                Email = "kialagabrielaaa@gmail.com",
                CodeName = "Killa2k20",
            };

            user.SetPassword("kialabriel");

            bool result = await _userRepository.Create(user);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterUpdateUserFromDb()
        {
            // Don't forget to change this ID for an existent
            var userId = new Guid("29a785b4-8c8c-40ef-9bfe-3a01f76dec91");

            var user = await _userRepository.Get(userId);
            user.Name = "Kiala Campos Gabriel";
            user.LastModified = DateTime.UtcNow;

            bool result = await _userRepository.Update(user);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterDeleteUserFromDb()
        {
            // Don't forget to change this ID for an existent
            var userId = new Guid("0af5f376-be8a-4014-9cd4-75be42ae0232");
            var user = await _userRepository.Get(userId);

            bool result = await _userRepository.Delete(user);
            Assert.IsTrue(result);
        }
    }
}