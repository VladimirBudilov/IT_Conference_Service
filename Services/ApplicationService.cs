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
            //TODO check that unsant application exist -> return exception
            //TODO check that author ID added
            //TODO checkthat at least one additional field added

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
            //TODO after update cant have less then fields id and one additional field
            //TODO cant update sent application
            //TODO cant update unexist application


            applicationModel.CreatedAt = DateTime.Now.ToUniversalTime();
            var application = await _unitOfWork.ApplicationRepository.GetByIdWithDetailsAsNoTrackingAsync(id);
            var info = application.AuthorInfo;
            
            _mapper.Map(applicationModel, application);
            _mapper.Map(applicationModel, info);
            _unitOfWork.ApplicationRepository.Update(application);
            _unitOfWork.SpeackerInfoRepository.Update(info);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task DeleteApplication(Guid id)
        {
            //TODO cant delete sent application
            //TODO cant delete unexist application 

            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id);
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
            if (application.IsSent == false)
            {
                //TODO add castom exception
            }
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task<IEnumerable<ActivityModel>> GetActivities()
        {
            var applications = await _unitOfWork.ApplicationRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ActivityModel>>(applications);
        }
    }
}
