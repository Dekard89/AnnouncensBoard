using AnnoucensBoard.Domain.Entity;
using AnnoucensBoard.Domain.Filters;

namespace AnnoucensBoard.Domain
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);

        Task<ICollection<T>> GetAllTopic(TopicFilter topicFilter);

        Task<ICollection<Subject>> GetAllSubjects(SubjectFilter subjectFilter);

        Task<ICollection<T>> GetByPage(int pageNumber, int pageSize);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<string> AddSomeTopic(List<Topic> topics);
        
    }
}
