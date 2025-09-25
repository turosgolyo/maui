namespace Solution.Validators;

public class MotorcycleModelValidator : BaseValidator<MotorcycleModel>
{
    public static string ModelProperty => nameof(MotorcycleModel.Model);
    public static string CubicProperty => nameof(MotorcycleModel.Cubic);
    public static string ManufacturerProperty => nameof(MotorcycleModel.Manufacturer);
    public static string NumberOfCylindersProperty => nameof(MotorcycleModel.NumberOfCylinders);
    public static string ReleaseYearProperty => nameof(MotorcycleModel.ReleaseYear);
    public static string TypeProperty => nameof(MotorcycleModel.Type);
    public static string GlobalProperty => "Global";

    public MotorcycleModelValidator(IHttpContextAccessor httpContextAccessor = null) : base(httpContextAccessor)
    {
        if(IsPutMethod)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required for update operations");
            //validalni hogy a ID letezik-e az adatbazisban
        }

        RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required");

        RuleFor(x => x.Cubic).NotNull().WithMessage("Cubic is required")
                             .GreaterThan(0).WithMessage("Cubic has to be greater then 0");

        RuleFor(x => x.Manufacturer).NotNull().WithMessage("Manufacturer must not be null.")
                                    .DependentRules(() =>
                                    {
                                        RuleFor(x => x.Manufacturer.Id).GreaterThan(0).WithMessage("Manufacturer id must be greater than 0");
                                    });
        //validalni hogy a Manufacturer ID letezik-e az adatbazisban

        RuleFor(x => x.Type).NotNull().WithMessage("Type is required")
                                      .DependentRules(() =>
                                      {
                                          RuleFor(x => x.Type.Id).GreaterThan(0).WithMessage("Type id must be greater than 0");
                                      });
        //validalni hogy a type ID letezik-e az adatbazisban

        RuleFor(x => x.NumberOfCylinders).NotNull().WithMessage("Number of cylinders is required")
                                         .InclusiveBetween(1, 8).WithMessage("Number of cylynders has to be between 1 and 8");

        RuleFor(x => x.ReleaseYear).NotNull().WithMessage("Release year is required")
                                   .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Invalid release year");
    }

}
