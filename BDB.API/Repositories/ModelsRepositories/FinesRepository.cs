namespace BDB.API.Repositories.ModelsRepositories
{
    public class FinesRepository : RepositoryBase<Fine>
    {
        public FinesRepository(AppDb context) : base(context) { }

        public async Task<List<Fine>> GetAllFinesAsync(bool trackChanges) =>
            await GetAll(trackChanges).ToListAsync();

        public async Task<List<Fine>> GetCarFinesAsync(string carNumber, bool trackChanges) =>
            await GetByCondition(f => f.CarNumber.Equals(carNumber), trackChanges).ToListAsync();
        
        public async Task<Fine?> GetFineByIdAsync(Guid id, bool trackChanges) =>
            await GetByCondition(f => f.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void CreateFine(Fine fine) => Create(fine);

        public void DeleteFine(Fine fine) => Delete(fine);
    }
}
