using Solution.Core.Interfaces;
using Solution.Services;

namespace Solution.Api.Configurations;

public static class DIConfigurations
{
    public static WebApplicationBuilder ConfigureDI(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IMotorcycleService, MotorcycleService>();

        return builder;
    }
}
