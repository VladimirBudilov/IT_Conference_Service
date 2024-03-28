using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using IT_Conference_Service.Services.Interfaces;
using IT_Conference_Service.Services.Models;

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
            applicationModel.Id = applicationModel.AuthorId;
            await _unitOfWork.ApplicationRepository.CreateAsync(_mapper.Map<Application>(applicationModel));
            await _unitOfWork.SaveAsync();
            return applicationModel;
        }

        public async Task<ApplicationModel> GetApplication(Guid id)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task<ApplicationModel> UpdateApplication(Guid id, ApplicationModel applicationModel)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            application = _mapper.Map(applicationModel, application);
            _unitOfWork.ApplicationRepository.Update(application);
            await _unitOfWork.SaveAsync();
            return applicationModel;
        }

        public async Task DeleteApplication(Guid id)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            _unitOfWork.ApplicationRepository.Delete(application);
        }

        public async Task<ApplicationModel> SendApplicationOnReview(Guid id)
        {
            // TODO add validation
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            application.IsSent = true;
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ApplicationModel>(application);
        }

        Task<IEnumerable<ApplicationModel>> IApplicationService.GetAllAfterData(DateTime date)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ApplicationModel>> IApplicationService.GetAllUnsubmittedAfterData(DateTime date)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationModel> IApplicationService.GetUnsubmittedApplication(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ApplicationModel>> IApplicationService.GetActivities()
        {
            throw new NotImplementedException();
        }
    }
}
