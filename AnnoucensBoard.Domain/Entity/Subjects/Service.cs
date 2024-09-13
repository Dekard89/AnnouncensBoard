using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoucensBoard.Domain.Entity.Subjects
{
    public class Service : Subject
    {
        public TimeSpan LeadTime { get; set; }
    }
}
