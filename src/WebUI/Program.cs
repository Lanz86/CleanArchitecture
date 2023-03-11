using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.WebUI.Commons;
using FastEndpoints;
using FastEndpoints.ClientGen;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.UseFastEndpoints(cfg => 
{
    cfg.Endpoints.ShortNames = true;
    cfg.Endpoints.RoutePrefix = "api";

});
app.UseSwaggerGen();

app.MapRazorPages();

app.MapFallbackToFile("index.html");

await app.GenerateClientsAndExitAsync();

app.Run();