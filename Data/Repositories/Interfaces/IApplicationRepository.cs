using IT_Conference_Service.Data.Entitiess;

namespace IT_Conference_Service.Data.Repositories.Interfaces
{
    public interface IApplicationRepository : ICrudRepository<Application>
    {
        Task<Application> GetFullDataByIdAsync(Guid id);
        Task<Application> GetFullDataByIdAsNoTrackingAsync(Guid id);
    }
}
