namespace Solution.Core.Models;

public partial class ArtistModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    [ObservableProperty]
    [JsonPropertyName("albums")]
    private List<AlbumModel> albums = new List<AlbumModel>();

    [ObservableProperty]
    [JsonPropertyName("songs")]
    private List<SongModel> songs = new List<SongModel>();

    public ArtistModel()
    {
    }

    public ArtistModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public ArtistModel(ArtistEntity entity)
    {
        if(entity is null)
        {
            return;
        }

        Id = entity.Id;
        Name = entity.Name;
    }

    public ArtistEntity ToEntity()
    {
        return new ArtistEntity
        {
            Name = Name,
            Id = Id,
        };
    }

    public void ToEntity(ArtistEntity entity)
    {
        entity.Name = Name;
        entity.Id = Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is ArtistModel model &&
            this.Id == model.Id &&
            this.Name == model.Name &&
            this.Albums.SequenceEqual(model.Albums) &&
            this.Songs.SequenceEqual(model.Songs);
    }
}
