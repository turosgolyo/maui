
namespace Solution.Validators;

public class ManufacturerModelValidator : BaseValidator<ManufacturerModel>
{
    public static string NameProperty => nameof(ManufacturerModel.Name);
    public static string GlobalProperty => "Global";

    public ManufacturerModelValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Manufacturer ID is required for update operations");
        }

        RuleFor(x => x.Name).NotEmpty().WithMessage("Manufacturer name is required!");
    }

}
