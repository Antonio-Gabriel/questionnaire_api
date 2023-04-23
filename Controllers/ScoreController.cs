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
    public class ScoreController : ControllerBase
    {
        private ILogger _log;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private IScoreRepository _scoreRepository;
        public ScoreController(
            IMapper mapper,
            ILogger<ScoreController> log,
            IUserRepository userRepository,
            IScoreRepository scoreRepository
        )
        {
            _log = log;
            _mapper = mapper;
            _userRepository = userRepository;
            _scoreRepository = scoreRepository;
        }

        [HttpGet("scores")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ScoreResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var scores = _mapper.Map<List<ScoreResponse>>(await _scoreRepository.GetAll());

            return Ok(scores);
        }

        [HttpGet("score/{id}")]
        [ProducesResponseType(200, Type = typeof(ScoreResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var score = _mapper.Map<ScoreResponse>(await _scoreRepository.Get(id));

            return Ok(score);
        }

        [HttpPost("score")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(
            [FromBody] ScoreRequestDto scoreRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            var score = _mapper.Map<Score>(scoreRequest);

            if (!await _userRepository.Exists(score.UserId))
            {
                _log.LogInformation("User already have a score base");
                return BadRequest("User already have a score base");
            }

            if (!await _scoreRepository.Create(score))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                _log.LogError("Something went wrong while saving");

                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("score/{id}/update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] ScoreRequestDto scoreRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            if (!await _scoreRepository.Exists(id))
            {
                _log.LogInformation("Score don't exists");
                return NotFound("Score don't exists");
            }

            var score = await _scoreRepository.Get(id);
            score.Correct = scoreRequest.Correct;
            score.Wrong = scoreRequest.Wrong;
            score.LastModified = DateTime.UtcNow;

            if (!await _scoreRepository.Update(score))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                _log.LogError("Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("score/{id}/delete")]
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

            if (!await _scoreRepository.Exists(id))
            {
                _log.LogInformation("Score don't exists");
                return NotFound("Score don't exists");
            }

            var score = _mapper.Map<Score>(await _scoreRepository.Get(id));

            if (!await _scoreRepository.Delete(score))
            {
                _log.LogError("Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}