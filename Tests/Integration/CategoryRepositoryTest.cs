using NUnit.Framework;

namespace Tests.Integration
{
    public class CategoryRepositoryTest
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTest()
        {
            var _context = DataContextFactory.create();
            _categoryRepository = new CategoryRepository(_context);
        }

        [Test]
        public async Task ReturnCategoryLists()
        {
            var categories = await _categoryRepository.GetAll();
            Assert.IsNotNull(categories);
        }

        [Test]
        public async Task ReturnTrueAfterInsertCategoryFromDb()
        {
            var category = new Category { Name = "Test Category" };            
            bool result = await _categoryRepository.Create(category);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterUpdateCategoryFromDb()
        {            
            // Don't forget to change this ID for an existent
            var categoryId = new Guid("97514a90-0d18-43ff-817e-9f2ea5c9b0e0");

            var category = await _categoryRepository.Get(categoryId);
            category.Name = "Enginnering";            
            category.LastModified = DateTime.UtcNow;

            bool result = await _categoryRepository.Update(category);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterDeleteCategoryFromDb()
        { 
            // Don't forget to change this ID for an existent
            var categoryId = new Guid("12a3c73f-b63c-4e63-b2b5-1a4c457d3194");
            var category = await _categoryRepository.Get(categoryId);

            bool result = await _categoryRepository.Delete(category);
            Assert.IsTrue(result);
        }
    }
}