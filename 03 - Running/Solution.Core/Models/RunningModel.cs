
using MauiValidationLibrary;
using MauiValidationLibrary.ValidationRules;
using Solution.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace Solution.Core.Models;

public partial class RunningModel
{
    public string Id { get; set; }
    public ValidatableObject<DateTime> Date { get; protected set; }
    public ValidatableObject<double> Distance { get; protected set; }
    public ValidatableObject<double> AverageSpeed { get; protected set; }
    public ValidatableObject<int> BurnedCalories { get; protected set; }
    public ValidatableObject<int> Time { get; protected set; }

    public RunningModel() 
    {
        this.Date = new ValidatableObject<DateTime>();
        this.Distance = new ValidatableObject<double>();
        this.AverageSpeed = new ValidatableObject<double>();
        this.BurnedCalories = new ValidatableObject<int>();
        this.Time = new ValidatableObject<int>();

        AddValidators();
    }

    public RunningModel(RunningEntity entity): this()
    {
        Id = entity.PublicId;
        Date.Value = entity.Date;
        Distance.Value = entity.Distance;
        AverageSpeed.Value = entity.AverageSpeed;
        BurnedCalories.Value = entity.BurnedCalories;
        Time.Value = entity.Time;
    }

    public RunningEntity ToEntity()
    {
        return new RunningEntity
        {
            PublicId = Id,
            Date = Date.Value,
            Distance = Distance.Value,
            AverageSpeed = AverageSpeed.Value,
            BurnedCalories = BurnedCalories.Value,
            Time = Time.Value
        };
    }

    public void ToEntity(RunningEntity entity)
    {
        entity.PublicId = Id;
        entity.Date = Date.Value;
        entity.Distance = Distance.Value;
        entity.AverageSpeed = AverageSpeed.Value;
        entity.BurnedCalories = BurnedCalories.Value;
        entity.Time = Time.Value;
    }

    private void AddValidators()
    { 
        this.Distance.Validations.Add(new NullableIntegerRule<double> { ValidationMessage = "Distance is required field." });
        this.Distance.Validations.Add(new MinValueRule<double>(0) { ValidationMessage = "Length can't bee less then 0." });

        this.AverageSpeed.Validations.Add(new NullableIntegerRule<double> { ValidationMessage = "Average speed is required field." });
        this.AverageSpeed.Validations.Add(new MinValueRule<double>(0) { ValidationMessage = "Average speed can't bee less then 0." });

        this.BurnedCalories.Validations.Add(new NullableIntegerRule<int> { ValidationMessage = "Burned calories is required field." });
        this.BurnedCalories.Validations.Add(new MinValueRule<int>(0) { ValidationMessage = "Burned calories can't bee less then 0." });

        this.Time.Validations.Add(new NullableIntegerRule<int> { ValidationMessage = "Time is required field." });
        this.Time.Validations.Add(new MinValueRule<int>(0) { ValidationMessage = "Time can't bee less then 0." });
    }
}
