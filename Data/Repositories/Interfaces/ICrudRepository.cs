namespace IT_Conference_Service.Data.Repositories.Interfaces
{
    public interface ICrudRepository<T>
    {
        Task CreateAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
