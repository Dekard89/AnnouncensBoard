using AnnoucensBoard.Domain.Entity;
using AnnouncensBoard.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.Interfaces
{
    public interface IMapper<E,D> where E : BaseEntity where D : AbstractModel
    {
        E MappingToEntity(D dto);

        D MappingToDto(E entity);
    }
}
