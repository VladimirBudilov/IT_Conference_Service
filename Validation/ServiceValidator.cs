using AutoMapper;
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
            AuthorIdExist(applicationModel, "To create an application, the author ID must not be empty.");
            AllApplicationFieldsAreEmpty(applicationModel, "To create an application, At least one additional field must be added.");
            await AuthorHasDraft(applicationModel);
        }
        public async Task ApplicationCanBeUpdated(ApplicationModel applicationModel)
        {
            await ApplicationNotExist(applicationModel, "You can not update unexisted application");
            AuthorIdExist(applicationModel, "To update an application, the author ID must not be empty.");
            AllApplicationFieldsAreEmpty(applicationModel, "At least one additional field must be added.");
            await ApplicationWasSent(applicationModel, "You can not update applicationw which was sent");
        }
        public async Task ApplicationCanBeDeleted(ApplicationModel applicationModel)
        {
            await ApplicationNotExist(applicationModel, "You can not delete unexisted application");
            await ApplicationWasSent(applicationModel, "You can not delete applicationw which was sent");
        }
        public async Task ApplicationCaBeSant(ApplicationModel applicationModel)
        {
            await ApplicationNotExist(applicationModel, "You can not sent on approvement unexisted application");
            await ApplicationWasSent(applicationModel, "You can not sent applicationw which was already sent");
            var model = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(applicationModel.Id);
            _mapper.Map(model, applicationModel);
            ApplicationHasEmptyFields(applicationModel);
        }

        public async Task ApplicationWasSent(ApplicationModel applicationModel, string message)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(applicationModel.Id);
            if (application.IsSent)
            {
                throw new ServiceBehaviorException(message);
            }
        }
        public async Task ApplicationNotExist(ApplicationModel applicationModel, string message)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsyncAsNoTracking(applicationModel.Id);
            if (application == null)
            {
                throw new ServiceBehaviorException(message);
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

        public void AuthorIdExist(ApplicationModel applicationModel, string message)
        {
            if (applicationModel.AuthorId == Guid.Empty)
            {
                throw new ServiceBehaviorException(message);
            }
        }
        public void AllApplicationFieldsAreEmpty(ApplicationModel applicationModel, string message)
        {
            if (string.IsNullOrEmpty(applicationModel.ActivityName)
                && string.IsNullOrEmpty(applicationModel.Description)
                && string.IsNullOrEmpty(applicationModel.Outline))
            {
                throw new ServiceBehaviorException(message);
            }
        }
        public void ApplicationHasEmptyFields(ApplicationModel applicationModel)
        {
            if (string.IsNullOrEmpty(applicationModel.ActivityName)
                || string.IsNullOrEmpty(applicationModel.Description)
                || string.IsNullOrEmpty(applicationModel.Outline))
            {
                throw new ServiceBehaviorException("You can not send an application before all fields should be added.");
            }
        }
    }
}
