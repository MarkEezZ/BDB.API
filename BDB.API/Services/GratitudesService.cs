namespace BDB.API.Services
{
    public class GratitudesService
    {
        public void RegisterService(WebApplication app)
        {
            app.MapGet("/gratitudes", async (IRepositoryManager repo) => await GetAllGratitudesAsync(repo));
            app.MapGet("/gratitudes/{id}", async (IRepositoryManager repo, int id) => await GetGratitudeByIdAsync(repo, id));
            app.MapPost("/gratitudes/add", async (IRepositoryManager repo, [FromBody] GratitudeDto gratitudeData) => await CreateGratitudeAsync(repo, gratitudeData));
            app.MapDelete("/gratitudes/{id}/delete", async (IRepositoryManager repo, int id) => await DeleteGratitudeAsync(repo, id));
        }

        private async Task<IResult> GetAllGratitudesAsync(IRepositoryManager repo)
        {
            List<Gratitude> gratitudes = await repo.Gratitudes.GetAllGratitudesAsync(trackChanges: false);
            return Results.Ok(gratitudes);
        }

        private async Task<IResult> GetGratitudeByIdAsync(IRepositoryManager repo, int id)
        {
            Gratitude? gratitudeFromDb = await repo.Gratitudes.GetGratitudeByIdAsync(id, trackChanges: false);
            if (gratitudeFromDb != null)
                return Results.Ok(gratitudeFromDb);

            return Results.NotFound($"The gratitude with ID '{id}' was not found.");
        }

        private async Task<IResult> CreateGratitudeAsync(IRepositoryManager repo, GratitudeDto gratitudeData)
        {
            Gratitude gratitude = new Gratitude();
            Mapper.Map(gratitude, gratitudeData);
            gratitude.Date = DateTime.Now;

            repo.Gratitudes.CreateGratitude(gratitude);
            await repo.SaveAsync();

            return Results.Created($"/gratitudes/{gratitude.Id}", gratitude);
        }

        private async Task<IResult> DeleteGratitudeAsync(IRepositoryManager repo, int id)
        {
            Gratitude? gratitude = await repo.Gratitudes.GetGratitudeByIdAsync(id, trackChanges: false);
            if (gratitude != null)
            {
                repo.Gratitudes.DeleteGratitude(gratitude);
                await repo.SaveAsync();

                return Results.Ok($"The gratitude has been sucsessfully deleted.");
            }
            return Results.NotFound($"The gratitude with id '{id}' was not found.");
        }
    }
}
