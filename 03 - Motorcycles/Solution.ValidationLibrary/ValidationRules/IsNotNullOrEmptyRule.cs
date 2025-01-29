﻿namespace Solution.ValidationLibrary.ValidationRules;

public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }

    public bool Check(object value) => value is string str && !string.IsNullOrWhiteSpace(str);
}
