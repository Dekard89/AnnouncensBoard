using AnnoucensBoard.Domain.Entity;
using AnnoucensBoard.Domain.Entity.Subjects;
using AnnouncensBoard.BLL.DTO;
using AnnouncensBoard.BLL.DTO.Subjects;
using AnnouncensBoard.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.Services
{
    public class SubjectMapper : IMapper<Subject, SubjectDTO>
    {
        public SubjectDTO MappingToDto(Subject entity)
        {
            if(entity is Product)
            {
                var dto = new ProductDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    AdultOnly = entity.AdultOnly,
                    Price = entity.Price,
                    Discription = entity.Discription,
                    Quantity = (entity as Product).Quantity,
                    Characteristics = entity.Characteristics.Select(x => new CharacteristicDTO
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Value = x.Value
                    }).ToList()

                };
            
               return dto;
            }
            else
            {
                var dto = new ServiceDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    AdultOnly = entity.AdultOnly,
                    Price = entity.Price,
                    Discription = entity.Discription,
                    LeadTime = (entity as Service).LeadTime,
                    Characteristics = entity.Characteristics.Select(x => new CharacteristicDTO
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Value = x.Value
                    }).ToList()


                };
                return dto;
            }
        }

        public Subject MappingToEntity(SubjectDTO dto)
        {
            if(dto is ProductDto)
            {
                var entity = new Product
                {
                    Title = dto.Title,
                    AdultOnly = dto.AdultOnly,
                    Price = dto.Price,
                    Discription = dto.Discription,
                    Quantity = (dto as ProductDto).Quantity,
                    Characteristics = dto.Characteristics.Select(x => new Characteristic
                    {
                        Title = x.Title,
                        Value = x.Value
                    }).ToList()
                };
                return entity;
            }
            else
            {
                var entity = new Service
                {
                    Title = dto.Title,
                    AdultOnly = dto.AdultOnly,
                    Price = dto.Price,
                    Discription = dto.Discription,
                    LeadTime = (dto as ServiceDto).LeadTime,
                    Characteristics = dto.Characteristics.Select(x => new Characteristic
                    {
                        Title = x.Title,
                        Value = x.Value
                    }).ToList()
                };
                return entity;
            }
        }
    }
}

