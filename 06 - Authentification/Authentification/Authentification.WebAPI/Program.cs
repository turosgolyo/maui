var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.ConfigureDI()
       .LoadEnvironmentVariables()
       .ConfigureDatabase()
       .LoadSettings()
       .UseSecurity()
       .UseIdentity()
       .UseScalarOpenAPI()
       .UseSwashbuckleOpenAPI()
       .UseScalarOpenAPI();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSecurity();
app.MapControllers();
app.UseScalarOpenAPI();
//app.UseSwashbuckleOpenAPI();
//app.UseReDocOpenAPI();


await app.RunAsync();
