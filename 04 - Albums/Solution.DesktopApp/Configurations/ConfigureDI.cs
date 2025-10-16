namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<CreateOrEditArtistViewModel>();
        builder.Services.AddTransient<CreateOrEditAlbumViewModel>();
        builder.Services.AddTransient<CreateOrEditSongViewModel>();
        builder.Services.AddTransient<SongListViewModel>();
        builder.Services.AddTransient<AlbumListViewModel>();
        builder.Services.AddTransient<ArtistListViewModel>();

        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<CreateOrEditArtistView>();
        builder.Services.AddTransient<CreateOrEditAlbumView>();
        builder.Services.AddTransient<CreateOrEditSongView>();
        builder.Services.AddTransient<SongListView>();
        builder.Services.AddTransient<AlbumListView>(); 
        builder.Services.AddTransient<ArtistListView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService>();
        builder.Services.AddTransient<IAlbumService, AlbumService>();
        builder.Services.AddTransient<IArtistService, ArtistService>();
        builder.Services.AddTransient<ISongService, SongService>();

        return builder;
	}
}
