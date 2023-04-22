using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionaryApp.Application.Dtos.Request;
using QuestionaryApp.Application.Dtos.Response;

namespace QuestionaryApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuestionnaireController : Controller
    {
        private ILogger _log;
        private IMapper _mapper;
        private IQuestionnaireRepository _questionnaireRepository;

        public QuestionnaireController(
            IMapper mapper,
            ILogger<QuestionnaireController> log,
            IQuestionnaireRepository questionnaireRepository
        )
        {
            _log = log;
            _mapper = mapper;
            _questionnaireRepository = questionnaireRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuestionnaireResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var questionnaires = _mapper.Map<List<QuestionnaireResponse>>(await _questionnaireRepository.GetAll());

            return Ok(questionnaires);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(QuestionnaireResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var questionnaire = _mapper.Map<QuestionnaireResponse>(await _questionnaireRepository.Get(id));

            return Ok(questionnaire);
        }

        [HttpGet("{questionaireId}/Questions")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuestionResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetQuestionsOfQuestionnaire(Guid questionaireId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var questions = _mapper.Map<List<QuestionResponse>>(
                await _questionnaireRepository
                    .GetQuestionnaireQuestions(questionaireId)
                    );

            return Ok(questions);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(
            [FromBody] QuestionnaireRequestDto questionnaireRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            };

            var questionnaireExists = await _questionnaireRepository
                .GetByTitle(questionnaireRequest.Title);

            if (questionnaireExists)
            {
                _log.LogInformation("Questionnaire already exists");
                return BadRequest("Questionnaire already exists");
            }

            var questionnaire = _mapper.Map<Questionnaire>(questionnaireRequest);

            if (!await _questionnaireRepository.Create(questionnaire))
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
            Guid id, [FromBody] QuestionnaireRequestDto questionnaireRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            if (!await _questionnaireRepository.Exists(id))
            {
                _log.LogInformation("Questionnaire don't exists");
                return NotFound("Questionnaire don't exists");
            }

            var questionnaire = await _questionnaireRepository.Get(id);
            questionnaire.Title = questionnaireRequest.Title;
            questionnaire.CategoryId = questionnaireRequest.CategoryId;
            questionnaire.LastModified = DateTime.UtcNow;

            if (!await _questionnaireRepository.Update(questionnaire))
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

            if (!await _questionnaireRepository.Exists(id))
            {
                _log.LogInformation("Questionnaire don't exists");
                return NotFound("Questionnaire don't exists");
            }

            var questionnaire = _mapper.Map<Questionnaire>(
                await _questionnaireRepository.Get(id)
                );

            if (!await _questionnaireRepository.Delete(questionnaire))
            {
                _log.LogError("Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}