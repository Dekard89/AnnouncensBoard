using AnnoucensBoard.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoucensBoard.Domain.Filters
{
    public record TopicFilter(
         string Titles,
         double SubjectPrice,
         string Categories
        );

}
