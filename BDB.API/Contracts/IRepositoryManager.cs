using BDB.API.Repositories.ModelsRepositories;

namespace BDB.API.Repositories
{
    public interface IRepositoryManager
    {
        DetectorsRepository Detectors { get; }
        FinesRepository Fines { get; }
        AdminsRepository Admins { get; }
        GratitudesRepository Gratitudes { get; }
        Task SaveAsync();
    }
}
