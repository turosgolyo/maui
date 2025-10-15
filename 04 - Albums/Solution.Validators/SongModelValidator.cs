
namespace Solution.Validators;

public class SongModelValidator : BaseValidator<SongModel>
{
    public static string NameProperty => nameof(SongModel.Name);
    public static string ArtistProperty => nameof(SongModel.Artist);
    public static string AlbumProperty => nameof(SongModel.Album);
    public static string DurationProperty => nameof(SongModel.Duration);
    public static string GlobalProperty => "Global";

    public SongModelValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Song ID is required for update operations");
        }

        RuleFor(x => x.Name).NotEmpty().WithMessage("Song name is required!");
        RuleFor(x => x.Artist).NotNull().WithMessage("Artist is required!");
        RuleFor(x => x.Album).NotNull().WithMessage("Album is required!");
        RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required!");
    }

}
