namespace Solution.ValidationLibrary.ValidationRules;

public class PickerValidationRule<T> : IValidationRule<T> where T : IObjectValidator<uint>
{
    public string ValidationMessage { get; set; } = "Manufacturer must be selected!";

    public bool Check(object value)
    {
        var isTypeOf = value is T;
        var isNull = value is T;

        if(!isTypeOf || isNull)
        {
            return false;
        }

        if(value is IObjectValidator<uint> objectValidator)
        {
            return objectValidator.Id != 0 &&
                   isTypeOf && 
                   !isNull;
        }

        return false;
    }
}
