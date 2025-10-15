namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
        builder.Services.AddTransient<MainViewModel>();

        builder.Services.AddTransient<MainView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService>();
        builder.Services.AddTransient<IAlbumService, AlbumService>();
        builder.Services.AddTransient<IArtistService, ArtistService>();
        builder.Services.AddTransient<ISongService, SongService>();

        return builder;
	}
}
