using QuestionaryApp.Domain.Events.Contract;

namespace QuestionaryApp.Domain.Events
{
    public class ChoosedCorrectAnswerEvent
    {

        public ChoosedCorrectAnswerEvent(CorrectAnswerEventContract contract)
        {
            Contract = contract;
        }

        public CorrectAnswerEventContract Contract { get; }
    }
}