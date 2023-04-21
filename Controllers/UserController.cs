using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionaryApp.Application.Dtos.Request;
using QuestionaryApp.Application.Dtos.Response;

namespace QuestionaryApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger _log;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserController(
            IMapper mapper,
            ILogger<UserController> log,
            IUserRepository userRepository
        )
        {
            _log = log;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = _mapper.Map<List<UserResponse>>(await _userRepository.GetAll());

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _mapper.Map<UserResponse>(await _userRepository.Get(id));

            return Ok(user);
        }
        
        [HttpGet("{id}/Score")]
        [ProducesResponseType(200, Type = typeof(ScoreResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserScores(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userScores = _mapper.Map<ScoreResponse>(await _userRepository.GetUserScore(id));

            return Ok(userScores);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(
            [FromBody] UserRequestDto userRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            };

            var userExists = await _userRepository.GetUserByEmail(userRequest.Email);
            if (userExists != null)
            {
                _log.LogInformation("User already exists");
                return BadRequest("User already exists");
            }

            var user = _mapper.Map<User>(userRequest);
            user.SetPassword(user.Password);

            if (!EmailValidator.IsValid(user.Email)) return BadRequest("Invalid email, please verify");

            if (!await _userRepository.Create(user))
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
            Guid id, [FromBody] UserRequestDto userRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            if (!await _userRepository.Exists(id))
            {
                _log.LogInformation("User don't exists");
                return NotFound("User don't exists");
            }

            var user = await _userRepository.Get(id);
            user.Name = userRequest.Name;
            user.Email = userRequest.Email;
            user.SetPassword(userRequest.Password);
            user.CodeName = userRequest.CodeName;
            user.LastModified = DateTime.UtcNow;

            if (!await _userRepository.Update(user))
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

            if (!await _userRepository.Exists(id))
            {
                _log.LogInformation("User don't exists");
                return NotFound("User don't exists");
            }

            var user = _mapper.Map<User>(await _userRepository.Get(id));

            if (!await _userRepository.Delete(user))
            {
                _log.LogError("Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}