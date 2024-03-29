using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using IT_Conference_Service.Services.Models;

namespace IT_Conference_Service.Validation
{
    public class ServiceValidator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceValidator(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ApplicationCanBeCreated(ApplicationModel applicationModel)
        {
            AuthorIdExist(applicationModel);
            AllApplicationFieldsAreEmpty(applicationModel);
            await AuthorHasDraft(applicationModel);
        }
        public async Task ApplicationCanBeUpdated(ApplicationModel applicationModel)
        {
            AuthorIdExist(applicationModel);
            AllApplicationFieldsAreEmpty(applicationModel);
            await ApplicationNotExist(applicationModel);
            await ApplicationWasSent(applicationModel);
        }
        public async Task ApplicationCanBeDeleted(ApplicationModel applicationModel)
        {
            await ApplicationNotExist(applicationModel);
            await ApplicationWasSent(applicationModel);
        }
        public async Task ApplicationCaBeSant(ApplicationModel applicationModel)
        {
            await ApplicationNotExist(applicationModel);
            var model = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(applicationModel.Id);
            _mapper.Map(model, applicationModel);
            ApplicationHasEmptyFields(applicationModel);
            await ApplicationWasSent(applicationModel);
        }

        public async Task ApplicationWasSent(ApplicationModel applicationModel)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(applicationModel.Id);
            if (application.IsSent)
            {
                throw new ServiceBehaviorException("You can't update sent application");
            }
        }
        public async Task ApplicationNotExist(ApplicationModel applicationModel)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsyncAsNoTracking(applicationModel.Id);
            if (application == null)
            {
                throw new ServiceBehaviorException("Application does not exist.");
            }
        }
        public async Task AuthorHasDraft(ApplicationModel applicationModel)
        {
            var application = await _unitOfWork.ApplicationRepository.GetDraft(applicationModel.AuthorId);
            if (application == null) return;
            if (!application.IsSent)
            {
                throw new ServiceBehaviorException("You can't create application. You have unsent draft");
            }
        }

        public void AuthorIdExist(ApplicationModel applicationModel)
        {
            if (applicationModel.AuthorId == Guid.Empty)
            {
                throw new ServiceBehaviorException("Author ID must not be empty.");
            }
        }
        public void AllApplicationFieldsAreEmpty(ApplicationModel applicationModel)
        {
            if (string.IsNullOrEmpty(applicationModel.ActivityName)
                && string.IsNullOrEmpty(applicationModel.Description)
                && string.IsNullOrEmpty(applicationModel.Outline)
                && applicationModel.ActivityType == ActivityType.None.ToEnumMemberString())
            {
                throw new ServiceBehaviorException("At least one additional field must be added.");
            }
        }
        public void ApplicationHasEmptyFields(ApplicationModel applicationModel)
        {
            if (string.IsNullOrEmpty(applicationModel.ActivityName)
                || string.IsNullOrEmpty(applicationModel.Description)
                || string.IsNullOrEmpty(applicationModel.Outline)
                || applicationModel.ActivityType == ActivityType.None.ToEnumMemberString())
            {
                throw new ServiceBehaviorException("All fields shuold be added.");
            }
        }
    }
}
