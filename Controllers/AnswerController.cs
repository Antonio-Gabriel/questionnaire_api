using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QuestionaryApp.Application.Dtos.Request;
using QuestionaryApp.Application.Dtos.Response;

namespace QuestionaryApp.Controllers
{
    [ApiController]
    [Route("api/v1")]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private ILogger _log;
        private IMapper _mapper;
        private IAnswerRepository _answerRepository;
        public AnswerController(
            IMapper mapper,
            ILogger<AnswerController> log,
            IAnswerRepository answerRepository
        )
        {
            _log = log;
            _mapper = mapper;
            _answerRepository = answerRepository;
        }

        [HttpGet("answers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnswerResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var answers = _mapper.Map<List<AnswerResponse>>(await _answerRepository.GetAll());

            return Ok(answers);
        }

        [HttpGet("answer/{id}")]
        [ProducesResponseType(200, Type = typeof(AnswerResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var question = _mapper.Map<AnswerResponse>(await _answerRepository.Get(id));

            return Ok(question);
        }

        [HttpPost("answer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(
            [FromBody] AnswerRequestDto answerRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            };

            var answerExists = await _answerRepository.GetByTitle(answerRequest.Text);
            if (answerExists)
            {
                _log.LogInformation("Answer already exists");
                return BadRequest("Answer already exists");
            }

            var totalAnswers = await _answerRepository
                .GetTotalAnswersByQuestion(answerRequest.QuestionId);

            if (totalAnswers == (int)QuestionnaireRangeLimits.AnswersPerQuestion)
            {
                _log.LogInformation("Reached the limit of 4 answers");
                return BadRequest("Reached the limit of 4 answers per question");
            }

            var answer = _mapper.Map<Answer>(answerRequest);

            if (!await _answerRepository.Create(answer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                _log.LogError("Something went wrong while savin");

                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("answer/{id}/update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] AnswerRequestDto answerRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            if (!await _answerRepository.Exists(id))
            {
                _log.LogInformation("Answer don't exists");
                return NotFound("Answer don't exists");
            }

            var answer = await _answerRepository.Get(id);
            answer.Text = answerRequest.Text;
            answer.QuestionId = answerRequest.QuestionId;
            answer.LastModified = DateTime.UtcNow;

            if (!await _answerRepository.Update(answer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                _log.LogError("Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("answer/{id}/delete")]
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

            if (!await _answerRepository.Exists(id))
            {
                _log.LogInformation("Question don't exists");
                return NotFound("Question don't exists");
            }

            var answer = _mapper.Map<Answer>(await _answerRepository.Get(id));

            if (!await _answerRepository.Delete(answer))
            {
                _log.LogError("Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}