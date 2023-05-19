namespace BDB.API.Repositories.ModelsRepositories
{
    public class AdminsRepository : RepositoryBase<Admin>
    {
        public AdminsRepository(AppDb context) : base(context) { }

        public async Task<List<Admin>> GetAllAdminsAsync(bool trackChanges) =>
            await GetAll(trackChanges).ToListAsync();

        public async Task<Admin?> GetAdminByIdAsync(Guid id, bool trackChanges) =>
            await GetByCondition(d => d.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<Admin?> GetAdminByLoginAsync(string login, bool trackChanges) =>
            await GetByCondition(d => d.Login.Equals(login), trackChanges).SingleOrDefaultAsync();

        public void UpdateAdmin(Admin admin) => Update(admin);

        public void CreateAdmin(Admin admin) => Create(admin);

        public void DeleteAdmin(Admin admin) => Delete(admin);
    }
}
