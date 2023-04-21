using NUnit.Framework;

namespace Tests.Unit
{
    public class QuestionnaireEntityTest
    {
        [Test]
        public void TestTheRelationshipBetweenEntitiesIfReturnsAValidData()
        {
            var questionnaire = new Questionnaire
            {
                Title = "Software Enginnering Common Questions for Interview",
                Category = new Category
                {
                    Name = "Enginnering"
                }
            };

            Assert.That(questionnaire.Id, Is.Not.Null);
            Assert.AreEqual("Enginnering", questionnaire.Category.Name);
        }
    }
}