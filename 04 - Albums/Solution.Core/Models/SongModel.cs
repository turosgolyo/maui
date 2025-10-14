namespace Solution.Core.Models;

public partial class SongModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    [ObservableProperty]
    [JsonPropertyName("duration")]
    private string duration;

    [ObservableProperty]
    [JsonPropertyName("artist")]
    private ArtistModel artist;

    [ObservableProperty]
    [JsonPropertyName("album")]
    private AlbumModel album;



    public SongModel()
    {
        this.Id = id;
        this.Name = name;
        this.Duration = duration;
        this.Artist = artist;
        this.Album = album;
    }

    public SongModel(SongEntity entity)
    {
        if (entity is null)
        {
            return;
        }

        Id = entity.Id;
        Name = entity.Name;
        Duration = entity.Duration;
        Artist = new ArtistModel(entity.Artist);
        Album = new AlbumModel(entity.Album);
    }

    public SongEntity ToEntity()
    {
        return new SongEntity
        {
            Name = Name,
            Id = Id,
            Duration = Duration,
            ArtistId = Artist.Id,
            AlbumId = Album.Id
        };
    }

    public void ToEntity(SongEntity entity)
    {
        entity.Name = Name;
        entity.Id = Id;
        entity.Duration = Duration;
        entity.ArtistId = Artist.Id;
        entity.AlbumId = Album.Id;
    }
}