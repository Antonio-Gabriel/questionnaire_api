using NUnit.Framework;

namespace Tests.Unit
{
    public class QuestionsEntityTest
    {
        List<Question>? _question;
        Questionnaire? _questionnaire;

        [SetUp]
        public void Setup()
        {
            _questionnaire = new Questionnaire
            {
                Title = "Software Enginnering Common Questions for Interview",
                Category = new Category
                {
                    Name = "Enginnering"
                }
            };

            _question = new List<Question> {
                new Question {
                    Title = "How do you understand about complexity of problems",
                    Questionnaire = _questionnaire
                },
                new Question {
                    Title = "What is the main of Object-Oriented Programming",
                    Questionnaire = _questionnaire
                }
            };
        }

        [Test]
        public void TestIfQuestionsAreJoinedIntoAQuestionnaire()
        {
            Assert.AreEqual(2, _question!.Count());
            Assert.IsInstanceOf<List<Question>>(_question);
            Assert.AreEqual(
                "How do you understand about complexity of problems",
                _question!.First().Title
                );
        }
    }
}