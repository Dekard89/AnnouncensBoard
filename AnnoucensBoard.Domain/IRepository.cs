using AnnoucensBoard.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoucensBoard.Domain
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById<T>(int id);

        Task<ICollection<T>> GetAll();

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(int id);

        Task<T> DeleteAsync(int id);
    }
}
