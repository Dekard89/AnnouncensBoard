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
    public class SubjectsController : ControllerBase
    {
        private IRepository<Topic> _repository;
        private readonly IValidator<SubjectDTO> _validator;
        private readonly ILogger<SubjectsController> _logger;
        private readonly IMapper<Subject, SubjectDTO> _mapper;
        private readonly IAuthorizationService _service;

        public SubjectsController(IRepository<Topic> repository,
            IMapper<Subject, SubjectDTO> mapper, ILogger<SubjectsController> logger,
            IValidator<SubjectDTO> validator,
            IAuthorizationService service)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> Get(SubjectFilter filter)
        {
            var subjects = await _repository.GetAllSubjects(filter);

            var result = subjects.Select(x => _mapper.MappingToDto(x)).ToList();

            return Ok(result);
        }

    }
}
