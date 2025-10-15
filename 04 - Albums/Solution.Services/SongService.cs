namespace Solution.Services;

public class SongService(AppDbContext dbContext) : ISongService
{
    private int ROW_COUNT = 10;
    public async Task<ErrorOr<SongModel>> CreateAsync(SongModel model)
    {
        bool exists = await dbContext.Songs.AnyAsync(x => x.Name == model.Name);

        if (exists)
        {
            return Error.Conflict(description: "Song already exists!");
        }

        var song = model.ToEntity();

        await dbContext.Songs.AddAsync(song);
        await dbContext.SaveChangesAsync();

        return new SongModel(song);
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int id)
    {
        var result = await dbContext.Songs.AsNoTracking()
                                          .Where(x => x.Id == id)
                                          .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<SongModel>> GetByIdAsync(int id)
    {
        var song = await dbContext.Songs.FirstOrDefaultAsync(x => x.Id == id);

        if(song is null)
        {
            return Error.NotFound(description: "Song not found");
        }

        return new SongModel(song);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(SongModel model)
    {
        var result = await dbContext.Songs.AsNoTracking()
                                          .Where(x => x.Id == model.Id)
                                          .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<List<SongModel>>> GetAllAsync() => await dbContext.Songs.Select(x => new SongModel(x))
                                                                                      .ToListAsync();

    public async Task<ErrorOr<PaginationModel<SongModel>>> GetPagedAsync(int page = 0)
    {
        page = page <= 0 ? 1 : page - 1;

        var songs = await dbContext.Songs.AsNoTracking()
                                         .Skip(page * ROW_COUNT)
                                         .Take(ROW_COUNT)
                                         .Select(x => new SongModel(x))
                                         .ToListAsync();

        var paginationModel = new PaginationModel<SongModel>
        {
            Items = songs,
            Count = await dbContext.Songs.CountAsync()
        };

        return paginationModel;
    }
}
