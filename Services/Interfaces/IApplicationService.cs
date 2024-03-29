using IT_Conference_Service.Services.Models;

namespace IT_Conference_Service.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationModel> CreateApplication(ApplicationModel applicationModel);
        Task<ApplicationModel> UpdateApplication(Guid id, ApplicationModel applicationModel);
        Task DeleteApplication(Guid id);
        Task<ApplicationModel> SendApplicationOnReview(Guid id);
        Task<IEnumerable<ApplicationModel>> GetAllAfterData(DateTime date);
        Task<IEnumerable<ApplicationModel>> GetAllUnsubmittedBeforeData(DateTime date);
        Task<ApplicationModel> GetUnsubmittedApplication(Guid id);
        Task<ApplicationModel> GetApplication(Guid id);
        Task <IEnumerable<ApplicationModel>> GetApplications();
        Task<IEnumerable<ActivityModel>> GetActivities();
    }
}