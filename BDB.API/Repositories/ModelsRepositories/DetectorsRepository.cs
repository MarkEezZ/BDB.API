namespace BDB.API.Repositories.ModelsRepositories
{
    public class DetectorsRepository: RepositoryBase<Detector>
    {
        public DetectorsRepository(AppDb context) : base(context) { }

        public async Task<List<Detector>> GetAllDetectorsAsync(bool trackChanges) =>
            await GetAll(trackChanges).ToListAsync();

        public async Task<Detector?> GetDetectorByIdAsync(int id, bool trackChanges) =>
            await GetByCondition(d => d.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void UpdateDetector(Detector detector) => Update(detector);

        public void CreateDetector(Detector detector) => Create(detector);

        public void DeleteDetector(Detector detector) => Delete(detector);
    }
}
