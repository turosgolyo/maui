
namespace Solution.Validators;

public class ArtistModelValidator : BaseValidator<ArtistModel>
{
    public static string NameProperty => nameof(ArtistModel.Name);
    public static string GlobalProperty => "Global";

    public ArtistModelValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Artist ID is required for update operations");
        }

        RuleFor(x => x.Name).NotEmpty().WithMessage("Artist name is required!");
    }

}
