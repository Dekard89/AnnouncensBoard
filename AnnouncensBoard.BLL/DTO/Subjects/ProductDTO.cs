using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.DTO.Subjects
{
    public record ProductDto : SubjectDTO
    {
        [Required(ErrorMessage = "Is required")]
        [Range(1, 100, ErrorMessage = "Out of range")]
        public int Quantity { get; set; }
    }
}
