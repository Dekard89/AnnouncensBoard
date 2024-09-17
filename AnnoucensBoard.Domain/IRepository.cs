using AnnoucensBoard.Domain.Entity;
using AnnoucensBoard.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoucensBoard.Domain
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);

        Task<ICollection<T>> GetAllTopic(TopicFilter topicFilter);

        Task<ICollection<T>> GetAllSubjects(SubjectFilter subjectFilter);

        Task<ICollection<T>> GetByPage(int id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(int id);

        Task<T> DeleteAsync(int id);

        Task AddSomeTopic();
    }
}
