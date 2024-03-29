using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using IT_Conference_Service.Services.Interfaces;
using IT_Conference_Service.Validation;

namespace IT_Conference_Service.Services.Models
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ServiceValidator _validator;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, ServiceValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApplicationModel> GetApplication(Guid id)
        {
            await _validator.ApplicationNotExist(new ApplicationModel { Id = id });
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetaiksAsync(id);
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task<ApplicationModel> CreateApplication(ApplicationModel applicationModel)
        {
            await _validator.ApplicationCanBeCreated(applicationModel);
            applicationModel.Id = Guid.NewGuid();
            applicationModel.UpdatedAt = DateTime.Now;
            await _unitOfWork.ApplicationRepository.CreateAsync(_mapper.Map<Application>(applicationModel));
            await _unitOfWork.SaveAsync();
            return applicationModel;
        }

        public async Task<ApplicationModel> UpdateApplication(Guid id, ApplicationModel applicationModel)
        {
            applicationModel.Id = id;
            await _validator.ApplicationCanBeUpdated(applicationModel);
            applicationModel.UpdatedAt = DateTime.Now;
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(id);
            var info = application.ApplicationInfo;
            _mapper.Map(applicationModel, application);
            _mapper.Map(applicationModel, info);
            _unitOfWork.ApplicationRepository.Update(application);
            _unitOfWork.SpeackerInfoRepository.Update(info);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task DeleteApplication(Guid id)
        {
            await _validator.ApplicationCanBeDeleted(new ApplicationModel { Id = id });
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(id);
            _unitOfWork.SpeackerInfoRepository.Delete(application.ApplicationInfo);
            _unitOfWork.ApplicationRepository.Delete(application);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ApplicationModel> SendApplicationOnReview(Guid id)
        {
            await _validator.ApplicationCaBeSant(new ApplicationModel { Id = id });
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            application.IsSent = true;
            application.SentAt = DateTime.Now;
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task<IEnumerable<ApplicationModel>> GetAllAfterData(DateTime date)
        {
            var applications = await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ApplicationModel>>(applications.Where(x => x.IsSent && x.SentAt > date));
        }

        public async Task<IEnumerable<ApplicationModel>> GetAllUnsubmittedBeforeData(DateTime date)
        {
            var applications = await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ApplicationModel>>(applications.Where(x => !x.IsSent && x.UpdatedAt < date));
        }

        public async Task<ApplicationModel> GetUnsubmittedApplication(Guid authorId)
        {
            var application = await _unitOfWork.ApplicationRepository.GetDraft(authorId);
            if (application == null)
            {
                throw new ServiceBehaviorException("You don't have unsent application.");
            }
            var model = _mapper.Map<ApplicationModel>(application);
            await _validator.ApplicationWasSent(model);
            return model;
        }

        public async Task<IEnumerable<ActivityModel>> GetActivities()
        {
            var applications = await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ActivityModel>>(applications);
        }

        public async Task<IEnumerable<ApplicationModel>> GetApplications()
        {
            return _mapper.Map<IEnumerable<ApplicationModel>>(await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync());
        }
    }
}
