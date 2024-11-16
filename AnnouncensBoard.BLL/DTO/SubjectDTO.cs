using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.DTO
{
    public record SubjectDTO : AbstractModel
    {
        public double Price { get; set; }
        public string Discription { get; set; } = string.Empty;

        public bool AdultOnly { get; set; }
        public List<CharacteristicDTO> Characteristics { get; set; } = new();

    }
}
