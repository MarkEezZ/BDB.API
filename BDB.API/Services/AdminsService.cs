using BDB.API.Contracts;

namespace BDB.API.Services
{
    public class AdminsService : IService
    {
        public void RegisterService(WebApplication app)
        {
            app.MapGet("/admins", async (IRepositoryManager repo) => await GetAllAdminsAsync(repo));
            app.MapGet("/admins/{id}", async (IRepositoryManager repo, string id) => await GetAdminByIdAsync(repo, id));
            app.MapPost("/admins/add", async (IRepositoryManager repo, [FromBody] AdminDto adminData) => await CreateAdminAsync(repo, adminData));
            app.MapPut("/admins/{id}/change", async (IRepositoryManager repo, string id, [FromBody] AdminDto adminData) => await ChangeAdminAsync(repo, id, adminData));
            app.MapDelete("/admins/{id}/delete", async (IRepositoryManager repo, string id) => await DeleteAdminAsync(repo, id));
        }

        private async Task<IResult> GetAllAdminsAsync(IRepositoryManager repo)
        {
            List<Admin> admins = await repo.Admins.GetAllAdminsAsync(trackChanges: false);
            return Results.Ok(admins);
        }

        private async Task<IResult> GetAdminByIdAsync(IRepositoryManager repo, string id)
        {
            Admin? adminFromDb = await repo.Admins.GetAdminByIdAsync(Guid.Parse(id), trackChanges: false);
            if (adminFromDb != null)
                return Results.Ok(adminFromDb);

            return Results.NotFound($"The admin with ID '{id}' was not found.");
        }

        private async Task<IResult> CreateAdminAsync(IRepositoryManager repo, AdminDto adminData)
        {
            Admin? adminFromDb = await repo.Admins.GetAdminByLoginAsync(adminData.Login, trackChanges: false);
            if (adminFromDb == null)
            {
                Admin admin = new Admin();
                Mapper.Map(admin, adminData);

                repo.Admins.CreateAdmin(admin);
                await repo.SaveAsync();

                return Results.Created($"/admins/{admin.Id}", admin);
            }
            return Results.BadRequest($"The admin with login '{adminData.Login}' already exists.");
        }

        private async Task<IResult> ChangeAdminAsync(IRepositoryManager repo, string id, AdminDto adminData)
        {
            Admin? admin = await repo.Admins.GetAdminByIdAsync(Guid.Parse(id), trackChanges: false);
            if (admin != null)
            {
                Mapper.Map(admin, adminData);

                repo.Admins.UpdateAdmin(admin);
                await repo.SaveAsync();

                return Results.Ok($"The admin has been sucsessfully updated.");
            }
            return Results.NotFound($"The admin with ID '{id}' was not found.");
        }

        private async Task<IResult> DeleteAdminAsync(IRepositoryManager repo, string id)
        {
            Admin? admin = await repo.Admins.GetAdminByIdAsync(Guid.Parse(id), trackChanges: false);
            if (admin != null)
            {
                repo.Admins.DeleteAdmin(admin);
                await repo.SaveAsync();

                return Results.Ok($"The admin has been sucsessfully deleted.");
            }
            return Results.NotFound($"The admin with ID '{id}' was not found.");
        }
    }
}
