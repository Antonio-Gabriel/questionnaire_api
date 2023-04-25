using NUnit.Framework;

using QuestionaryApp.Gateway;
using QuestionaryApp.Gateway.Models;

namespace Tests.e2e
{
    public class QuestionnaireGatewayTest
    {
        private QuestionnairesGateway _gateway;
        public QuestionnaireGatewayTest()
        {
            _gateway = new QuestionnairesGateway();
        }

        [Test]
        public async Task TestGatewayRequests()
        {
            var (questions, answers) = await _gateway.GetRequestsData();

            Assert.IsInstanceOf<List<AnswerModel>?>(answers);
            Assert.IsInstanceOf<List<QuestionModel>?>(questions);
        }
    }
}