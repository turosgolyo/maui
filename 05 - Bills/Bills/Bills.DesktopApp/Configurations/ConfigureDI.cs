using Bills.Core.Interfaces;
using Bills.Services;

namespace Bills.DesktopApp.Configurations;

public static class ConfigureDI
{
    public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IItemService, ItemService>();
        builder.Services.AddTransient<IBillService, BillService>();
    
        return builder;
    }
}
