using AnnouncensBoard.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncensBoard.Infrastructure.Interfaces
{
    public interface ICrudService
    {
        public Task CreateAsync(SubjectDTO subjectDto, CancellationToken cancellationToken);

        public Task DeleteAsync (SubjectDTO subjectDto, CancellationToken cancellationToken);

        public Task UpdateAsync(SubjectDTO subjectDto, CancellationToken cancellationToken);

        public Task<SubjectDTO> GetAsyncById(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<SubjectDTO>> GetAll(CancellationToken cancellationToken);
    }
}
