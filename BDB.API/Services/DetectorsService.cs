using BDB.API.Contracts;
using BDB.API.Repositories;

namespace BDB.API.Services
{
    public class DetectorsService : IService
    {
        public void RegisterService(WebApplication app)
        {
            app.MapGet("/detectors", async (IRepositoryManager repo) => await GetAllDetectorsAsync(repo));
            app.MapGet("/detectors/{id}", async (IRepositoryManager repo, int id) => await GetDetectorByIdAsync(repo, id));
            app.MapPost("/detectors/add", async (IRepositoryManager repo, [FromBody] DetectorDto detectorData) => await CreateDetectorAsync(repo, detectorData));
            app.MapPut("/detectors/{id}/change", async (IRepositoryManager repo, int id, [FromBody] DetectorDto detectorData) => await ChangeDetectorAsync(repo, id, detectorData));
            app.MapDelete("/detectors/{id}/delete", async (IRepositoryManager repo, int id) => await DeleteDetectorAsync(repo, id));
        }

        private async Task<IResult> GetAllDetectorsAsync(IRepositoryManager repo)
        {
            List<Detector> detectors = await repo.Detectors.GetAllDetectorsAsync(trackChanges: false);
            return Results.Ok(detectors);
        }
            
        private async Task<IResult> GetDetectorByIdAsync(IRepositoryManager repo, int id)
        {
            Detector? detectorFromDb = await repo.Detectors.GetDetectorByIdAsync(id, trackChanges: false);
            if (detectorFromDb != null)
                return Results.Ok(detectorFromDb);

            return Results.NotFound($"The fine with ID '{id}' was not found.");
        }

        private async Task<IResult> CreateDetectorAsync(IRepositoryManager repo, DetectorDto detectorData)
        {
                Detector detector = new Detector();
                Mapper.Map(detector, detectorData);

                repo.Detectors.CreateDetector(detector);
                await repo.SaveAsync();

                return Results.Created($"/detectors/{detector.Id}", detector);
        }

        private async Task<IResult> ChangeDetectorAsync(IRepositoryManager repo, int id, DetectorDto detectorData)
        {
            Detector? detector = await repo.Detectors.GetDetectorByIdAsync(id, trackChanges: false);
            if (detector != null)
            {
                Mapper.Map(detector, detectorData);

                repo.Detectors.UpdateDetector(detector);
                await repo.SaveAsync();

                return Results.Ok($"The detector has been sucsessfully updated.");
            }
            return Results.NotFound($"The detector with id '{id}' was not found.");
        }

        private async Task<IResult> DeleteDetectorAsync(IRepositoryManager repo, int id)
        {
            Detector? detector = await repo.Detectors.GetDetectorByIdAsync(id, trackChanges: false);
            if (detector != null)
            {
                repo.Detectors.DeleteDetector(detector);
                await repo.SaveAsync();

                return Results.Ok($"The detector has been sucsessfully deleted.");
            }
            return Results.NotFound($"The detector with id '{id}' was not found.");
        }
    }
}
