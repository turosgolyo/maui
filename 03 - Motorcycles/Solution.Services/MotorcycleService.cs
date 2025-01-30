﻿namespace Solution.Services;

public class MotorcycleService(AppDbContext dbContext) : IMotorcycleService
{
    private const int ROW_COUNT = 25;
    public async Task<ErrorOr<MotorcycleModel>> CreateAsync(MotorcycleModel model)
    {
        bool exists = await dbContext.Motorcycles.AnyAsync(x => x.ManufacturerId == model.Manufacturer.Value.Id &&
                                                                x.Model.ToLower() == model.Model.Value.ToLower().Trim() &&
                                                                x.ReleaseYear == model.ReleaseYear.Value);
        if (exists) 
        {
            return Error.Conflict(description: "Motorcycle already exists!");
        }

        var motorcycle = model.ToEntity();
        motorcycle.PublicId = Guid.NewGuid().ToString();

        await dbContext.Motorcycles.AddAsync(motorcycle);
        await dbContext.SaveChangesAsync();

        return new MotorcycleModel(motorcycle)
        {
            Manufacturer = model.Manufacturer
        };
    }
    public async Task<ErrorOr<MotorcycleModel>> UpdateAsync(MotorcycleModel model)
    {
        var motorcycle = await dbContext.Motorcycles.Include(x => x.Manufacturer)
                                                    .FirstOrDefaultAsync(x => x.PublicId == model.Id);
        if (motorcycle is null)
        {
            return Error.NotFound(description: "Motorcycle not found!");

        }
        
        model.ToEntity();
        await dbContext.SaveChangesAsync();

        return new MotorcycleModel(motorcycle);
    }
    public async Task<ErrorOr<Success>> DeleteAsync(string motorcycleId)
    {
        var motorcycle = await dbContext.Motorcycles.Include(x => x.Manufacturer)
                                                    .FirstOrDefaultAsync(x => x.PublicId == motorcycleId);
        if (motorcycle is null)
        {
            return Error.NotFound(description: "Motorcycle not found!");
        }


        dbContext.Motorcycles.Remove(motorcycle);
        await dbContext.SaveChangesAsync();

        return Result.Success;
    }

    public async Task<ErrorOr<MotorcycleModel>> GetByIdAsync(string motorcycleId)
    {
        var motorcycle = await dbContext.Motorcycles.Include(x => x.Manufacturer)
                                            .FirstOrDefaultAsync(x => x.PublicId == motorcycleId);
        if (motorcycle is null)
        {
            return Error.NotFound(description: "Motorcycle not found!");
        }

        return new MotorcycleModel(motorcycle);
    }
    public async Task<ErrorOr<List<MotorcycleModel>>> GetAllAsync() => await dbContext.Motorcycles.AsNoTracking()
                                                                                                   .Include(x => x.Manufacturer)
                                                                                                   .Select(x => new MotorcycleModel(x))
                                                                                                   .ToListAsync();
    public async Task<ErrorOr<List<MotorcycleModel>>> GetPagedAsync(int page = 0) => await dbContext.Motorcycles.AsNoTracking()
                                                                                                                .Include(x => x.Manufacturer)
                                                                                                                .Skip(page * ROW_COUNT)
                                                                                                                .Take(ROW_COUNT)
                                                                                                                .Select(x => new MotorcycleModel(x))
                                                                                                                .ToListAsync();

}
