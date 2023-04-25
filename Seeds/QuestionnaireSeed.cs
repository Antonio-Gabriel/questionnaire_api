using QuestionaryApp.Data;
using QuestionaryApp.Gateway;

namespace QuestionaryApp.Seeds
{
    public class QuestionnaireSeed
    {
        private readonly DataContext _context;

        public QuestionnaireSeed(DataContext context)
        {
            _context = context;
        }

        public async Task SeedDataContext()
        {
            if (
                !_context.Questionnaires.Any() &&
                !_context.Questions.Any() &&
                !_context.Answers.Any()
            )
            {
                var questionnaireId = Guid.NewGuid();

                List<Answer> answers = new List<Answer>();
                List<Question> questions = new List<Question>();

                var user = new User
                {
                    Name = "António Gabriel",
                    Email = "antoniogabriel12@gmail.com",
                    CodeName = "AGdev",
                };

                user.SetPassword("antoniogabriel");

                var questionnaire = new Questionnaire
                {
                    Title = "Questions about enginnering and security",
                    Category = new Category
                    {
                        Name = "Development"
                    }
                };

                questionnaire.SetId(questionnaireId);

                var _gateway = new QuestionnairesGateway();
                var (questions_data, answers_data) = await _gateway.GetRequestsData();

                foreach (var question in questions_data!)
                {
                    var question_item = new Question
                    {
                        Title = question.title,
                        QuestionnaireId = questionnaireId
                    };

                    question_item.SetId(question.id);

                    questions.Add(question_item);
                }

                foreach (var answer in answers_data!)
                {
                    var answer_item = new Answer
                    {
                        Text = answer.text,
                        QuestionId = answer.question_id,
                        isCorrect = answer.is_correct,
                    };

                    answer_item.SetId(answer.id);

                    answers.Add(answer_item);
                }

                // Persist to database
                _context.Users.Add(user);
                _context.SaveChanges();

                _context.Questionnaires.AddRange(questionnaire);
                _context.SaveChanges();

                _context.Questions.AddRange(questions);
                _context.SaveChanges();

                _context.Answers.AddRange(answers);
                _context.SaveChanges();
            }
        }
    }
}