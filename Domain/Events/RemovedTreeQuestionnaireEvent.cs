using QuestionaryApp.Domain.Events.Contract;

namespace QuestionaryApp.Domain.Events
{
    public class RemovedTreeQuestionnaireEvent
    {

        public RemovedTreeQuestionnaireEvent(QuestionnaireEventContract contract)
        {
            Contract = contract;
        }

        public QuestionnaireEventContract Contract { get; }
    }
}