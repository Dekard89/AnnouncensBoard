namespace AnnouncensBoard.Infrasructures.Interfaces;

public interface ICRUDService<T> where T : class
{
    public Task<T> Add(T entity);
    
    public Task<T> Update(T entity);
    
    public Task<T> Delete(int id);
    
    public Task<IEnumerable<T>> GetAll();
    
    public Task<T> GetById(int id);
}