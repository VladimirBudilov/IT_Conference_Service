namespace IT_Conference_Service.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorInfoRepository SpeackerInfoRepository { get; }
        IApplicationRepository ApplicationRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
