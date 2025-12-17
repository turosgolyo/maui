namespace Bills.Validators;

public class ItemModelRequestValidator : BaseValidator<ItemModelRequest>
{
    public static string BillIdProperty => nameof(ItemModel.Name);
    public static string GlobalProperty => "Global";

    public ItemModelRequestValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
       RuleFor(x => x.BillId).NotNull().WithMessage("BillId is required!")
                             .GreaterThan(0).WithMessage("BillId must be greater than 0!");
    }
}
