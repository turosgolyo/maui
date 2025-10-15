using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;

namespace Solution.DesktopApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {

        var builder = MauiApp.CreateBuilder();
        var cs = builder.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Using DB connection: {cs}");
        builder.UseMauiApp<App>()
               .UseMauiCommunityToolkit(options =>
               {
                   options.SetShouldEnableSnackbarOnWindows(true);
               })
               .UseMauiCommunityToolkitMarkup()
               .ConfigureSyncfusionCore()
               .ConfigureSyncfusionToolkit()
               .UseFontConfiguration()
               .UseAppConfigurations()
               .UseAppSettingsMapping()
               .UseDIConfiguration()
               .UseMsSqlServer();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
