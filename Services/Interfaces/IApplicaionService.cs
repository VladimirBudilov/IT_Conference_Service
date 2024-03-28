using IT_Conference_Service.Services.Models;

namespace IT_Conference_Service.Services.Interfaces
{
    public interface IApplicaionService
    {
        Task<ApplicationModel> CreateApplication(ApplicationModel applicationModel);
        Task DeleteApplication(Guid id);
        Task<ApplicationModel> GetApplication(Guid id);
        Task<IEnumerable<ApplicationModel>> GetApplications();
        Task<ApplicationModel> UpdateApplication(Guid id, ApplicationModel applicationModel);
    }
}