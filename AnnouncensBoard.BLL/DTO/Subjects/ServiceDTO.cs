using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.BLL.DTO.Subjects
{
    public record ServiceDto : SubjectDTO
    {
        [Required(ErrorMessage ="field requered")]
        public TimeSpan LeadTime { get; set; } = new(0, 0, 0, 0);
    }
}
