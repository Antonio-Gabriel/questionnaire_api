using NUnit.Framework;

namespace Tests.Integration
{
    public class AnswerRepositoryTest
    {
        private readonly AnswerRepository _answerRepository;

        public AnswerRepositoryTest()
        {
            var _context = DataContextFactory.create();
            _answerRepository = new AnswerRepository(_context);
        }

        [Test]
        public async Task ReturnAnswerLists()
        {
            var answers = await _answerRepository.GetAll();
            Assert.IsNotNull(answers);
        }

        [Test]
        public async Task ReturnTrueAfterInsertAnswerFromDb()
        {
            var answer = new Answer
            {
                Text = "Specifying a configure file for example",
                isCorrect = true,
                QuestionId = new Guid("75b9335d-7b7f-4a8f-b5d6-a4c6fed95195")
            };

            bool result = await _answerRepository.Create(answer);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterUpdateAnswerFromDb()
        {
            // Don't forget to change this ID for an existent
            var answerId = new Guid("97514a90-0d18-43ff-817e-9f2ea5c9b0e0");

            var answer = await _answerRepository.Get(answerId);
            answer.Text = "Applying rules there";
            answer.isCorrect = false;
            answer.LastModified = DateTime.UtcNow;

            bool result = await _answerRepository.Update(answer);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterDeleteAnswerFromDb()
        {
            // Don't forget to change this ID for an existent
            var answerId = new Guid("12a3c73f-b63c-4e63-b2b5-1a4c457d3194");
            var answer = await _answerRepository.Get(answerId);

            bool result = await _answerRepository.Delete(answer);

            Assert.IsTrue(result);
        }
    }
}