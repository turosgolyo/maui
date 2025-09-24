namespace Solution.Services;

public class TypeService(AppDbContext dbContext) : ITypeService
{
    public async Task<ErrorOr<TypeModel>> CreateAsync(TypeModel model)
    {
        bool exists = await dbContext.Types.AnyAsync(x => x.Name == model.Name);

        if (exists)
        {
            return Error.Conflict(description: "Type already exists!");
        }

        var type = model.ToEntity();

        await dbContext.Types.AddAsync(type);
        await dbContext.SaveChangesAsync();

        return new TypeModel(type);
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int id)
    {
        var result = await dbContext.Types.AsNoTracking()
                                          .Where(x => x.Id == id)
                                          .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<TypeModel>> GetByIdAsync(int id)
    {
        var type = await dbContext.Types.FirstOrDefaultAsync(x => x.Id == id);

        if(type is null)
        {
            return Error.NotFound(description: "Type not found");
        }

        return new TypeModel(type);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(TypeModel model)
    {
        var result = await dbContext.Types.AsNoTracking()
                                          .Where(x => x.Id == model.Id)
                                          .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<List<TypeModel>>> GetAllAsync() => await dbContext.Types.Select(x => new TypeModel(x))
                                                                                      .ToListAsync();

}
