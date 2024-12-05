using Solution.Database.Entities;

namespace Solution.Core.Models;

public partial class MovieModel : ObservableObject
{
    [ObservableProperty]
    private string id;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private uint length;

    [ObservableProperty]
    private DateTime release;
     
    public MovieModel()
    {

    }

    public MovieModel(MovieEntity entity) //entitybol modelba
    {
        Id = entity.PublicId;
        Title = entity.Title;
        Length = entity.Length;
        Release = entity.Release;
    }

    public MovieEntity ToEntity() //modelbol entitybe
    {
        return new MovieEntity
        {
            PublicId = Id,
            Title = Title,
            Length = Length,
            Release = Release
        };
    }

    public void ToEntity(MovieEntity entity) //update entity
    {
        entity.PublicId = Id;
        entity.Title = Title;
        entity.Length = Length;
        entity.Release = Release;
    }
}
