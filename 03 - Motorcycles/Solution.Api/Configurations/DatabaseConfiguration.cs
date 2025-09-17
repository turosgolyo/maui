using Solution.DataBase;

namespace Solution.Api.Configurations;

public static class DatabaseConfiguration
{
    public static WebApplication ConfigureDatabase(this WebApplication builder)
    {

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AppDbContext<AppDbContext>(options =>
            options.UseLazyLoadingProxies()
                   .UseSqlServer(connectionString, options =>
                   {
                       options.MigrationAsembly(Solution.Database.AssemblyReference.Assembly);
                       options.EnableRetryOnFailure();
                       options.CommandTimeout(300);
                   })
            );
    
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    return builder;
    }
}
