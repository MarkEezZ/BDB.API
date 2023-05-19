using BDB.API.Repositories.ModelsRepositories;

namespace BDB.API.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private AppDb context;

        private DetectorsRepository detectorsRepository;
        private FinesRepository finesRepository;
        private AdminsRepository adminsRepository;
        private GratitudesRepository gratitudesRepository;

        public RepositoryManager(AppDb context)
        {
            this.context = context;
        }

        public DetectorsRepository Detectors
        {
            get
            {
                if (detectorsRepository == null)
                    detectorsRepository = new DetectorsRepository(context);
                return detectorsRepository;
            }
        }

        public FinesRepository Fines
        {
            get
            {
                if (finesRepository == null)
                    finesRepository = new FinesRepository(context);
                return finesRepository;
            }
        }

        public AdminsRepository Admins
        {
            get
            {
                if (adminsRepository == null)
                    adminsRepository = new AdminsRepository(context);
                return adminsRepository;
            }
        }

        public GratitudesRepository Gratitudes
        {
            get
            {
                if (gratitudesRepository == null)
                    gratitudesRepository = new GratitudesRepository(context);
                return gratitudesRepository;
            }
        }

        public Task SaveAsync() =>
            context.SaveChangesAsync();
    }
}
