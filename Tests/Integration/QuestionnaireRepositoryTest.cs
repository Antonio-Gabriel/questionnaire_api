using NUnit.Framework;

namespace Tests.Integration
{
    public class QuestionnaireRepositoryTest
    {
        private readonly QuestionnaireRepository _questionnaireRepository;

        public QuestionnaireRepositoryTest()
        {
            var _context = DataContextFactory.create();
            _questionnaireRepository = new QuestionnaireRepository(_context);
        }

        [Test]
        public async Task ReturnQuestionnaireLists()
        {
            var questionnaires = await _questionnaireRepository.GetAll();

            Assert.IsNotNull(questionnaires);
        }

        [Test]
        public async Task ReturnTrueAfterInsertQuestionnaireFromDb()
        {
            var questionnaire = new Questionnaire
            {
                Title = "Enginnering common questions interviews",
                CategoryId = new Guid("dc431edf-6eda-4445-8ce6-71a6f2486b49")
            };

            bool result = await _questionnaireRepository.Create(questionnaire);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterUpdateQuestionnaireFromDb()
        {
            // Don't forget to change this ID for an existent
            var questionnaireId = new Guid("56ebe3d1-8808-4c24-8588-a6f4f18f7562");

            var questionnaire = await _questionnaireRepository.Get(questionnaireId);
            questionnaire.Title = "Common questions interviews for Google";
            questionnaire.LastModified = DateTime.UtcNow;

            bool result = await _questionnaireRepository.Update(questionnaire);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterDeleteQuestionnaireFromDb()
        {
            // Don't forget to change this ID for an existent
            var questionnaireId = new Guid("12a3c73f-b63c-4e63-b2b5-1a4c457d3194");
            var questionnaire = await _questionnaireRepository.Get(questionnaireId);

            bool result = await _questionnaireRepository.Delete(questionnaire);

            Assert.IsTrue(result);
        }
    }
}