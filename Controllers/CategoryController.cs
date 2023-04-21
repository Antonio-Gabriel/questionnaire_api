using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionaryApp.Application.Dtos.Request;
using QuestionaryApp.Application.Dtos.Response;

namespace QuestionaryApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : Controller
    {
        private ILogger _log;
        private IMapper _mapper;
        private ICategoryRepository _categoryRepository;
        public CategoryController(
            IMapper mapper,
            ILogger<CategoryController> log,
            ICategoryRepository categoryRepository
        )
        {
            _log = log;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categories = _mapper.Map<List<CategoryResponse>>(await _categoryRepository.GetAll());

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = _mapper.Map<CategoryResponse>(await _categoryRepository.Get(id));

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(
            [FromBody] CategoryRequestDto categoryRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            };

            var isExistent = await _categoryRepository.CategoryAlreadyExists(categoryRequest.Name);
            if (isExistent)
            {
                _log.LogInformation("Category already exists");
                return BadRequest("Category already exists");
            }

            var category = _mapper.Map<Category>(categoryRequest);

            if (!await _categoryRepository.Create(category))
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
            Guid id, [FromBody] CategoryRequestDto categoryRequest
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogWarning("Something wrong, check the payload data");
                return BadRequest(ModelState);
            }

            if (!await _categoryRepository.Exists(id))
            {
                _log.LogInformation("Category don't exists");
                return NotFound("Category don't exists");
            }

            var category = await _categoryRepository.Get(id);
            category.Name = categoryRequest.Name;
            category.LastModified = DateTime.UtcNow;

            if (!await _categoryRepository.Update(category))
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

            if (!await _categoryRepository.Exists(id))
            {
                _log.LogInformation("Category don't exists");
                return NotFound("Category don't exists");
            }

            var category = _mapper.Map<Category>(await _categoryRepository.Get(id));

            if (!await _categoryRepository.Delete(category))
            {
                _log.LogError("Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}