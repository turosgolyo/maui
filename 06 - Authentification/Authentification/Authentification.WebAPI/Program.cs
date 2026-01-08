var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.ConfigureDI()
       .LoadEnvironmentVariables()
       .ConfigureDatabase()
       .LoadSettings()
       .UseSecurity()
       .UseIdentity();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSecurity();
app.MapControllers();


await app.RunAsync();
