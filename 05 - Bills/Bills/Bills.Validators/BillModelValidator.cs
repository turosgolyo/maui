using Bills.Core.Models;
using FluentValidation;

namespace Bills.Validators;

public class BillModelValidator : BaseValidator<BillModel>
{
    public static string NumberProperty => nameof(BillModel.Number);
    public static string DateProperty => nameof(BillModel.Date);
    public static string GlobalProperty => "Global";

    public BillModelValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
        RuleFor(x => x.Number).NotEmpty().WithMessage("Number is required!")
                              .MinimumLength(2).MaximumLength(32).WithMessage("Number must be in between 2 and 32 characters!");

        RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required!")
                            .LessThanOrEqualTo(DateTime.Today).WithMessage("Date cannot be in the future.");
    }
}
