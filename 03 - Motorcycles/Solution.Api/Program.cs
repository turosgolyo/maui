var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.LoadAppSettingsVariables();
builder.ConfigureDatabase();

builder.Services.AddControllers();
builder.ConfigureDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
