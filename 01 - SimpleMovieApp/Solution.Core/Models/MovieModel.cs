namespace Solution.Core.Models;

public partial class MovieModel : ObservableObject
{
    [ObservableProperty]
    private ValidatableObject<string> id;

    [ObservableProperty]
    private ValidatableObject<string> title;

    [ObservableProperty]
    private ValidatableObject<uint?> length;

    [ObservableProperty]
    private ValidatableObject<DateTime> release;
     
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
            Length = Length.HasValue ? Length.Value : 0,
            Release = Release
        };
    }

    public void ToEntity(MovieEntity entity) //update entity
    {
        entity.PublicId = Id;
        entity.Title = Title;
        entity.Length = Length.HasValue ? Length.Value : 0;
        entity.Release = Release;
    }
}
