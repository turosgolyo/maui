
namespace Solution.Validators;

public class AlbumModelValidator : BaseValidator<AlbumModel>
{
    public static string NameProperty => nameof(AlbumModel.Name);
    public static string GenreProperty => nameof(AlbumModel.Genre);
    public static string ReleaseDateProperty => nameof(AlbumModel.ReleaseDate);
    public static string ArtistProperty => nameof(AlbumModel.Artist);
    public static string GlobalProperty => "Global";

    public AlbumModelValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Album ID is required for update operations");
        }

        RuleFor(x => x.Name).NotEmpty().WithMessage("Album name is required!");
        RuleFor(x => x.Artist).NotNull().WithMessage("Artist is required!");
        RuleFor(x => x.ReleaseDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Release date cannot be in the future!");
        RuleFor(x => x.Genre).NotEmpty().WithMessage("Genre is required!");
    }

}
