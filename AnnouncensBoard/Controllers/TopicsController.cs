using AnnoucensBoard.Domain;
using AnnoucensBoard.Domain.Entity;
using AnnoucensBoard.Domain.Filters;
using AnnouncensBoard.BLL.DTO;
using AnnouncensBoard.BLL.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncensBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly IRepository<Topic> _repository;
        private readonly IValidator<TopicDTO> _validator;
        private readonly IMapper<Topic, TopicDTO> _mapper;
        private readonly ILogger<TopicsController> _logger;
        private readonly IAuthorizationService _service;

        public TopicsController(IRepository<Topic> repository,
            IValidator<TopicDTO> validator,
            IMapper<Topic, TopicDTO> mapper,
            ILogger<TopicsController> logger,
            IAuthorizationService service)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<TopicDTO>> Get([FromQuery] int Id)
        {
            if (Id == 0)
                return BadRequest();

            var topic = await _repository.GetById(Id);

            var authResult= await _service.AuthorizeAsync(User,topic.Subject, "AdultOnlyPolicy");

            if (authResult.Succeeded)
            {
                var response = _mapper.MappingToDto(topic);

                _logger.LogInformation($"get {topic.Title}");

                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> Get([FromQuery] TopicFilter filter)
        {
            var topics = await _repository.GetAllTopic(filter);

            var responce = topics.Select(x => _mapper.MappingToDto(x)).ToList();

            return Ok(responce);

     
        }
        [HttpGet("page")]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetPage([FromQuery] int pageNumber, int pageSize)
        {
            var topics = await _repository.GetByPage(pageNumber, pageSize);

            var responce= topics.Select(x=>_mapper.MappingToDto(x)).ToList().ToList();

            return Ok(responce);
        }
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] TopicDTO topic)
        {
            var authResult = await _service.AuthorizeAsync(User, topic, "OnlyOwnerEditPolicy");

            var result= await _validator.ValidateAsync(topic);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var topicEntity=await _repository.GetById(topic.Id);
            if (topicEntity == null)
                return BadRequest();

            if (authResult.Succeeded)
            {
                var updatedTopic = _mapper.MappingToEntity(topic);

                updatedTopic.CreateTime=DateTime.UtcNow;

                await _repository.UpdateAsync(updatedTopic);

                return Ok();
            }
            return BadRequest();
           
            
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] TopicDTO topic)
        {
            var authResult = await _service.AuthorizeAsync(User, topic, "OnlyOwnerEditPolicy");

            var result = await _validator.ValidateAsync(topic);
            if(!result.IsValid)
                return BadRequest(result.Errors);

            var topicEntity= await _repository.GetById(topic.Id);
            if(topicEntity == null)
                return BadRequest();

            if (authResult.Succeeded)
            {
                var deletedTopic = _mapper.MappingToEntity(topic);

                await _repository.DeleteAsync(deletedTopic);

                return Ok();
            }

            return BadRequest();

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TopicDTO topic, HttpContext context)
        {
            var result = await _validator.ValidateAsync(topic);

            if(!result.IsValid)
                return BadRequest(result.Errors);

            var createdTopic = _mapper.MappingToEntity(topic);

            createdTopic.Author = context.User.Claims.FirstOrDefault(x => x.Type == "UserName").Value;

            createdTopic.CreateTime = DateTime.UtcNow;

            await _repository.AddAsync(createdTopic);

            return Ok();
        }

    }
}
