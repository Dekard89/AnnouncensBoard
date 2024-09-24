using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoucensBoard.Domain.Filters
{
    public record SubjectFilter
        (
          string Title,
          string CharacteristicsTitle
        );
    
}
