using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionaryApp.Application.Dtos.Request;
using QuestionaryApp.Application.Dtos.Response;

namespace QuestionaryApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuestionController : Controller
    {
        private ILogger _log;
        private IMapper _mapper;
        private IQuestionRepository _questionRepository;
        public QuestionController(
            IMapper mapper,
            ILogger<QuestionController> log,
            IQuestionRepository questionRepository
        )
        {
            _log = log;
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuestionResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var questions = _mapper.Map<List<QuestionResponse>>(await _questionRepository.GetAll());

            return Ok(questions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(QuestionResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var question = _mapper.Map<QuestionResponse>(await _questionRepository.Get(id));

            return Ok(question);
        }

        [HttpGet("{questionId}/Answers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnswerResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetQuestionsOfQuestionnaire(Guid questionId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var answers = _mapper.Map<List<AnswerResponse>>(
                await _questionRepository
                    .GetAnswersOfQuestion(questionId)
                    );

            return Ok(answers);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(
            [FromBody] QuestionRequestDto questionRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            };

            var questionExists = await _questionRepository.GetQuestionByTitle(questionRequest.Title);
            if (questionExists != null)
            {
                _log.LogInformation("Question already exists");
                return BadRequest("Question already exists");
            }

            var totalQuestions = await _questionRepository
                .GetTotalQuestionsByQuestionnaire(questionRequest.QuestionnaireId);

            if (totalQuestions == (int)QuestionnaireRangeLimits.Questions)
            {
                _log.LogInformation("Reached the limit of 10 questions");
                return BadRequest("Reached the limit of 10 questions per questionnaire");
            }

            var question = _mapper.Map<Question>(questionRequest);

            if (!await _questionRepository.Create(question))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                _log.LogError("Something went wrong while savin");

                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] QuestionRequestDto questionRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            if (!await _questionRepository.Exists(id))
            {
                _log.LogInformation("Question don't exists");
                return NotFound("Question don't exists");
            }

            var question = await _questionRepository.Get(id);
            question.Title = questionRequest.Title;
            question.QuestionnaireId = questionRequest.QuestionnaireId;
            question.LastModified = DateTime.UtcNow;

            if (!await _questionRepository.Update(question))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                _log.LogError("Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            if (!await _questionRepository.Exists(id))
            {
                _log.LogInformation("Question don't exists");
                return NotFound("Question don't exists");
            }

            var question = _mapper.Map<Question>(await _questionRepository.Get(id));

            if (!await _questionRepository.Delete(question))
            {
                _log.LogError("Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}