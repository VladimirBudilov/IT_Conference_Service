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
            await _unitOfWork.ApplicationRepository.CreateAsync(_mapper.Map<Application>(applicationModel));
            await _unitOfWork.SaveAsync();
            return applicationModel;
        }

        public async Task<ApplicationModel> GetApplication(Guid id)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task<IEnumerable<ApplicationModel>> GetApplications()
        {
            return _mapper.Map<IEnumerable<ApplicationModel>>(await _unitOfWork.ApplicationRepository.GetAllAsync());
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
    }
}
