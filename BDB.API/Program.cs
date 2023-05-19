var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDb>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

new DetectorsService().RegisterService(app);
new FinesService().RegisterService(app);

app.UseHttpsRedirection();

app.Run();