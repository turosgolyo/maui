namespace Authentification.WebAPI.Configurations;

public static class DatabaseConfiguration
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder ConfigureDatabase()
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AuthentificationDbContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(connectionString, options =>
                       {
                           options.MigrationsAssembly(DomainAssemblyReference.Assembly);
                           options.EnableRetryOnFailure();
                           options.CommandTimeout(300);
                       })
                       //.LogTo(Console.WriteLine)
            );

            return builder;
        }
    }
}
