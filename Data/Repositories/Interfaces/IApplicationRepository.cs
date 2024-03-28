using IT_Conference_Service.Data.Entitiess;

namespace IT_Conference_Service.Data.Repositories.Interfaces
{
    public interface IApplicationRepository : ICrudRepository<Application>
    {
        Task<Application> GetByIdWithDetaiksAsync(Guid id);
        Task<Application> GetByIdWithDetailsAsNoTrackingAsync(Guid id);
        Task<IEnumerable<Application>> GetAllWithDetailsAsync();
    }
}
