namespace Solution.Core.Models;

public partial class AlbumModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("imageId")]
    private string imageId;

    [ObservableProperty]
    [JsonPropertyName("webContentLink")]
    private string webContentLink;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    [ObservableProperty]
    [JsonPropertyName("artist")]
    private ArtistModel artist;

    [ObservableProperty]
    [JsonPropertyName("songs")]
    private List<SongModel> songs;

    [ObservableProperty]
    [JsonPropertyName("releaseDate")]
    private DateTime releaseDate;

    [ObservableProperty]
    [JsonPropertyName("genre")]
    private string genre;

    public AlbumModel()
    {
        this.Artist = new ArtistModel();
        this.Songs = new List<SongModel>();
        this.Name = name;
        this.ReleaseDate = new DateTime();
        this.Genre = genre;
    }

    public AlbumModel(AlbumEntity entity)
    {
        this.Id = entity.Id;
        this.ImageId = entity.ImageId;
        this.WebContentLink = entity.WebContentLink;
        this.Name = entity.Name;
        this.Artist = new ArtistModel(entity.Artist);
        this.Songs = new List<SongModel>(entity.Songs.Select(s => new SongModel(s)));
        this.ReleaseDate = entity.ReleaseDate;
        this.Genre = entity.Genre;
    }

    public AlbumEntity ToEntity()
    {
        return new AlbumEntity
        {
            Id = Id,
            ImageId = ImageId,
            WebContentLink = WebContentLink,
            Name = Name,
            ArtistId = Artist.Id,
            Songs = new List<SongEntity>(Songs.Select(s => s.ToEntity())),
            ReleaseDate = ReleaseDate,
            Genre = Genre
        };
    }

    public void ToEntity(AlbumEntity entity)
    {
        entity.Id = Id;
        entity.ImageId = ImageId;
        entity.WebContentLink = WebContentLink;
        entity.Name = Name;
        entity.ArtistId = Artist.Id;
        entity.Songs = new List<SongEntity>(Songs.Select(s => s.ToEntity()));
        entity.ReleaseDate = ReleaseDate;
        entity.Genre = Genre;
    }
}
