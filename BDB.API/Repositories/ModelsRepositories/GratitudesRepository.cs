namespace BDB.API.Repositories.ModelsRepositories
{
    public class GratitudesRepository : RepositoryBase<Gratitude>
    {
        public GratitudesRepository(AppDb context) : base(context) { }

        public async Task<List<Gratitude>> GetAllGratitudesAsync(bool trackChanges) =>
            await GetAll(trackChanges).ToListAsync();

        public async Task<Gratitude?> GetGratitudeByIdAsync(int id, bool trackChanges) =>
            await GetByCondition(d => d.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void CreateGratitude(Gratitude gratitude) => Create(gratitude);

        public void DeleteGratitude(Gratitude gratitude) => Delete(gratitude);
    }
}
