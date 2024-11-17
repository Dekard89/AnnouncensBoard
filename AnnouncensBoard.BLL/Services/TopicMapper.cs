using AnnoucensBoard.Domain.Entity;
using AnnouncensBoard.BLL.DTO;
using AnnouncensBoard.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.Services
{
    public class TopicMapper : IMapper<Topic, TopicDTO>
    {
        private IMapper<Subject, SubjectDTO> _subjMapper;

        public TopicMapper(IMapper<Subject,SubjectDTO> subjectMapper)
        { 
            _subjMapper=subjectMapper;
        }
        public TopicDTO MappingToDto(Topic entity)
        {
            var dto = new TopicDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Author = entity.Author,
                CategoryDto =(CategoryDTO) entity.Category,
                CreatedTime=entity.CreateTime,
                Subject= _subjMapper.MappingToDto(entity.Subject)

            };
            return dto;
            
        }

        public Topic MappingToEntity(TopicDTO dto)
        {
            var entity = new Topic
            {
                Title = dto.Title,
                
                Category = (Category)dto.CategoryDto,
              
                Subject = _subjMapper.MappingToEntity(dto.Subject)
            };
            return entity;
        }
    }
}
