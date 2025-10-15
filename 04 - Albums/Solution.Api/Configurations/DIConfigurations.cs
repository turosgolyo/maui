namespace Solution.Api.Configurations;

public static class DIConfigurations
{
    public static WebApplicationBuilder ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IAlbumService, AlbumService>();
        builder.Services.AddTransient<IArtistService, ArtistService>();
        builder.Services.AddTransient<ISongService, SongService>();

        return builder;
    }
}
