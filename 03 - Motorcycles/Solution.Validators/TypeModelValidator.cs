namespace Solution.Validators;

public class TypeModelValidator : BaseValidator<TypeModel>
{
    public static string NameProperty => nameof(TypeModel.Name);
    public static string GlobalProperty => "Global";

    public TypeModelValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Type ID is required for update operations");
        }

        RuleFor(x => x.Name).NotEmpty().WithMessage("Type name is required!");
    }

}
