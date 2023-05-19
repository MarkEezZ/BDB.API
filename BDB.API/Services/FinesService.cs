using BDB.API.Contracts;

namespace BDB.API.Services
{
    public class FinesService : IService
    {
        public void RegisterService(WebApplication app)
        {
            app.MapGet("/fines", async (IRepositoryManager repo) => await GetAllFinesAsync(repo));
            app.MapGet("/fines/car_fines", async (IRepositoryManager repo, [FromQuery] string carNumber) => await GetFinesByCarNumberAsync(repo, carNumber));
            app.MapGet("/fines/{id}", async (IRepositoryManager repo, string id) => await GetFineByIdAsync(repo, id));
            app.MapPost("/fines/add", async (IRepositoryManager repo, [FromBody] FineDto fineData) => await AddFineAsync(repo, fineData));
            app.MapDelete("/fines/{id}/delete", async (IRepositoryManager repo, string id) => await DeleteFineAsync(repo, id));
        }

        private async Task<IResult> GetAllFinesAsync(IRepositoryManager repo)
        {
            List<Fine> fines = await repo.Fines.GetAllFinesAsync(trackChanges: false);
            return Results.Ok(fines);
        }   

        private async Task<IResult> GetFinesByCarNumberAsync(IRepositoryManager repo, string carNumber)
        {
            List<Fine> fines = await repo.Fines.GetCarFinesAsync(carNumber, trackChanges: false);
            return Results.Ok(fines);
        }

        private async Task<IResult> GetFineByIdAsync(IRepositoryManager repo, string id)
        {
            Fine? fineFromDb = await repo.Fines.GetFineByIdAsync(Guid.Parse(id), trackChanges: false);
            if (fineFromDb != null)
                return Results.Ok(fineFromDb);

            return Results.NotFound($"The fine with ID '{id}' was not found.");
        }

        private async Task<IResult> AddFineAsync(IRepositoryManager repo, FineDto fineData)
        {
            Detector? detector = await repo.Detectors.GetDetectorByIdAsync(fineData.DetectorId, trackChanges: false);
            if (detector != null)
            {
                Fine fine = new Fine();
                Mapper.Map(fine, fineData);

                fine.FineAmount = CountFineAmout(fineData.OverSpeed);
                fine.Date = DateTime.Now;

                repo.Fines.CreateFine(fine);
                await repo.SaveAsync();

                return Results.Created($"/fines/{fine.Id}", fine);
            }
            return Results.BadRequest($"The detector with ID \"{fineData.DetectorId}\" was not found.");
        }

        private async Task<IResult> DeleteFineAsync(IRepositoryManager repo, string id)
        {
            Fine? fineFromDb = await repo.Fines.GetFineByIdAsync(Guid.Parse(id), trackChanges: false);
            if (fineFromDb != null)
            {
                repo.Fines.DeleteFine(fineFromDb);
                await repo.SaveAsync();

                return Results.Ok($"The fine with ID \"{id}\" was sucsessfuly deleted.");
            }
            return Results.NotFound($"The fine with ID \"{id}\" was not found.");
        }

        private double CountFineAmout(double speed)
        {
            double coef = 2;
            return coef * speed;
        }
    }
}
