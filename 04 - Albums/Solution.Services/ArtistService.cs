namespace Solution.Services;

public class ArtistService(AppDbContext dbContext) : IArtistService
{
    private int ROW_COUNT = 10;
    public async Task<ErrorOr<ArtistModel>> CreateAsync(ArtistModel model)
    {
        bool exists = await dbContext.Artists.AnyAsync(x => x.Name == model.Name);

        if (exists)
        {
            return Error.Conflict(description: "Artist already exists!");
        }

        var artist = model.ToEntity();

        await dbContext.Artists.AddAsync(artist);
        await dbContext.SaveChangesAsync();

        return new ArtistModel(artist);
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int id)
    {
        var result = await dbContext.Artists.AsNoTracking()
                                            .Where(x => x.Id == id)
                                            .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<ArtistModel>> GetByIdAsync(int id)
    {
        var artist = await dbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);

        if(artist is null)
        {
            return Error.NotFound(description: "Artist not found");
        }

        return new ArtistModel(artist);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(ArtistModel model)
    {
        var result = await dbContext.Artists.AsNoTracking()
                                            .Where(x => x.Id == model.Id)
                                            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<List<ArtistModel>>> GetAllAsync() => await dbContext.Artists.Select(x => new ArtistModel(x))
                                                                                          .ToListAsync();

    public async Task<ErrorOr<PaginationModel<ArtistModel>>> GetPagedAsync(int page = 0)
    {
        page = page <= 0 ? 1 : page - 1;

        var types = await dbContext.Artists.AsNoTracking()
                                           .Skip(page * ROW_COUNT)
                                           .Take(ROW_COUNT)
                                           .Select(x => new ArtistModel(x))
                                           .ToListAsync();

        var paginationModel = new PaginationModel<ArtistModel>
        {
            Items = types,
            Count = await dbContext.Artists.CountAsync()
        };

        return paginationModel;
    }
}
