using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using IT_Conference_Service.Services.Interfaces;
using IT_Conference_Service.Services.Models;
using IT_Conference_Service.Validation;

namespace IT_Conference_Service.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApplicationModel> CreateApplication(ApplicationModel applicationModel)
        {
            await ApplicationCanBeCreated(applicationModel);
            applicationModel.Id = applicationModel.AuthorId;
            applicationModel.CreatedAt = DateTime.Now.ToUniversalTime();
            await _unitOfWork.ApplicationRepository.CreateAsync(_mapper.Map<Application>(applicationModel));
            await _unitOfWork.SaveAsync();
            return applicationModel;
        }

        public async Task<ApplicationModel> GetApplication(Guid id)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetaiksAsync(id);
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task<ApplicationModel> UpdateApplication(Guid id, ApplicationModel applicationModel)
        {
            await ApplicationCanBeUpdated(applicationModel);
            applicationModel.CreatedAt = DateTime.Now.ToUniversalTime();
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
            
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            await ApplicationCanBeDeleted(_mapper.Map<ApplicationModel>(application));
            _unitOfWork.ApplicationRepository.Delete(application);
        }

        public async Task<ApplicationModel> SendApplicationOnReview(Guid id)
        {
            // TODO can be sent only once
            // TODO cant send unexist application
            // TODO cant send application without all fields

            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            application.IsSent = true;
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task<IEnumerable<ApplicationModel>> GetAllAfterData(DateTime date)
        {
            var applications = await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ApplicationModel>>(applications.Where(x => x.CreatedAt > date));
        }

        public async Task<IEnumerable<ApplicationModel>> GetAllUnsubmittedAfterData(DateTime date)
        {
            var applications = await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ApplicationModel>>(applications.Where(x => x.IsSent == false && x.CreatedAt < date));
        }

        public async Task<ApplicationModel> GetUnsubmittedApplication(Guid id)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(id);
            var model = _mapper.Map<ApplicationModel>(application);
            await ApplicationWasSent(model);
            return model;
        }

        public async Task<IEnumerable<ActivityModel>> GetActivities()
        {
            var applications = await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ActivityModel>>(applications);
        }

        #region Validation Methods
        private async Task ApplicationCanBeCreated(ApplicationModel applicationModel)
        {
            //TODO check that unsant application exist -> return exception
            //TODO check that author ID added
            //TODO checkthat at least one additional field added

            await ApplicationExistAndWasNotSent(applicationModel);
            AuthorIdExist(applicationModel);
            ApplicationHasEmptyFields(applicationModel);
        }
        private async Task ApplicationCanBeUpdated(ApplicationModel applicationModel)
        {
            //TODO after update cant have less then fields id and one additional field
            //TODO cant update sent application
            //TODO cant update unexist application

            AuthorIdExist(applicationModel);
            ApplicationHasEmptyFields(applicationModel);
            await ApplicationWasSent(applicationModel);
            await ApplicationNotExist(applicationModel);
        }
        private async Task ApplicationCanBeDeleted(ApplicationModel applicationModel)
        {
            //TODO cant delete sent application
            //TODO cant delete unexist application

            await ApplicationWasSent(applicationModel);
            await ApplicationNotExist(applicationModel);
        }

        private async Task ApplicationWasSent(ApplicationModel applicationModel)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(applicationModel.Id);
            if (application.IsSent == true)
            {
                throw new ServiceBehaviorException("Application is already sent.");
            }
        }
        private async Task ApplicationNotExist(ApplicationModel applicationModel)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(applicationModel.Id);
            if (application == null)
            {
                throw new ServiceBehaviorException("Application does not exist.");
            }
        }
        private async  Task ApplicationExistAndWasNotSent(ApplicationModel applicationModel)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(applicationModel.Id);
            if (application.IsSent == false)
            {
                throw new ServiceBehaviorException("You can't create application. You have unsent draft");
            }
        }
   
        private void AuthorIdExist(ApplicationModel applicationModel)
        {
            if (applicationModel.AuthorId == Guid.Empty)
            {
                throw new ServiceBehaviorException("Author ID must not be empty.");
            }
        }
        private void ApplicationHasEmptyFields(ApplicationModel applicationModel)
        {
            if (string.IsNullOrEmpty(applicationModel.ActivityName)
                && string.IsNullOrEmpty(applicationModel.Description)
                && string.IsNullOrEmpty(applicationModel.Outline))
            {
                throw new ServiceBehaviorException("At least one additional field must be added.");
            }

        }
        #endregion
    }
}
