namespace IT_Conference_Service.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ISpeackerInfoRepository SpeackerInfoRepository { get; }
        IApplicationRepository ApplicationRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
