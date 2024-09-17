using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoucensBoard.Domain.Filters
{
    public record SubjectFilter
        (
          IEnumerable<string> Titles,
          IEnumerable<string> CharacteristicsTitles
        );
    
}
