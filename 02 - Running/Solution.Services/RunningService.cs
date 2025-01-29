using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Interfaces;
using Solution.Core.Models;
using Solution.Database.Entities;
using Solution.DataBase;

namespace Solution.Services;

public class RunningService(AppDbContext dbContext) : IRunningService
{
    public async Task<ErrorOr<RunningModel>> CreateAsync(RunningModel running)
    {
        var isRunningExists = await dbContext.RunningEntities.AnyAsync(x =>
            x.Distance == running.Distance.Value &&
            x.AverageSpeed == running.AverageSpeed.Value &&
            x.Date.Date == running.Date.Value.Date &&
            x.Time == running.Time.Value &&
            x.BurnedCalories == running.BurnedCalories.Value
        );

        if (isRunningExists)
        {
            return Error.Conflict(description: $"Running with the same data already exists");
        }

        running.Id = Guid.NewGuid().ToString();
        RunningEntity entity = running.ToEntity();

        await dbContext.RunningEntities.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return new RunningModel(entity);
    }

}
