using AnnoucensBoard.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoucensBoard.Domain.Filters
{
    public record TopicFilter
        (
         IEnumerable<string> Titles,
         IEnumerable<DateTime> Dates,
         IEnumerable<string> AuthorNames,
         IEnumerable<string> SubjectTitles,
         IEnumerable<double> SubjectPricts,
         IEnumerable<Category> Categories
         
        );

}
