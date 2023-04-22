using NUnit.Framework;

namespace Tests.Integration
{
    public class QuestionRepositoryTest
    {
        private readonly QuestionRepository _questionRepository;

        public QuestionRepositoryTest()
        {
            var _context = DataContextFactory.create();
            _questionRepository = new QuestionRepository(_context);
        }

        [Test]
        public async Task ReturnQuestionLists()
        {
            var questions = await _questionRepository.GetAll();

            Assert.IsNotNull(questions);
        }

        [Test]
        public async Task ReturnTrueAfterInsertQuestionFromDb()
        {
            var question = new Question
            {
                Title = "How do you do to deploy a serverless application",
                QuestionnaireId = new Guid("56ebe3d1-8808-4c24-8588-a6f4f18f7562")
            };

            bool result = await _questionRepository.Create(question);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterUpdateQuestionFromDb()
        {
            // Don't forget to change this ID for an existent
            var questionId = new Guid("ec452b93-f676-4953-8da0-8c25e0e10f95");

            var question = await _questionRepository.Get(questionId);
            question.Title = "What's the main fundamentals of object-oriented programmingn";
            question.LastModified = DateTime.UtcNow;

            bool result = await _questionRepository.Update(question);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterDeleteQuestionFromDb()
        {
            // Don't forget to change this ID for an existent
            var questionId = new Guid("12a3c73f-b63c-4e63-b2b5-1a4c457d3194");
            var questionnaire = await _questionRepository.Get(questionId);

            bool result = await _questionRepository.Delete(questionnaire);

            Assert.IsTrue(result);
        }
    }
}