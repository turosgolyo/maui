namespace Solution.Services;

public class AlbumService(AppDbContext dbContext) : IAlbumService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<AlbumModel>> CreateAsync(AlbumModel model)
    {
        bool exists = await dbContext.Albums.AnyAsync(x => x.ArtistId == model.Artist.Id &&
                                                           x.Name == model.Name.ToLower().Trim() &&
                                                           x.ReleaseDate == model.ReleaseDate);

        if (exists)
        {
            return Error.Conflict(description: "Albums already exists!");
        }

        var album = model.ToEntity();
        
        await dbContext.Albums.AddAsync(album);
        await dbContext.SaveChangesAsync();

        return new AlbumModel(album);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(AlbumModel model)
    {
        var albumEntity = await dbContext.Albums.Include(x => x.Songs)
                                                .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (albumEntity == null)
        {
            return Error.NotFound();
        }

        albumEntity.Name = model.Name;
        albumEntity.ArtistId = model.Artist.Id;
        albumEntity.ImageId = model.ImageId;
        albumEntity.WebContentLink = model.WebContentLink;
        albumEntity.ReleaseDate = model.ReleaseDate;
        albumEntity.Genre = model.Genre;

        albumEntity.Songs.Clear();
        foreach (var songModel in model.Songs)
        {
            albumEntity.Songs.Add(songModel.ToEntity());
        }

        dbContext.Albums.Update(albumEntity);
        await dbContext.SaveChangesAsync();

        return Result.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int id)
    {
        var result = await dbContext.Albums.AsNoTracking()
                                           .Include(x => x.Artist)
                                           .Include(x => x.Songs)
                                           .Where(x => x.Id == id)
                                           .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<AlbumModel>> GetByIdAsync(int id)
    {
        var album = await dbContext.Albums.Include(x => x.Artist)
                                          .Include(x => x.Songs)
                                          .FirstOrDefaultAsync(x => x.Id == id);

        if (album is null)
        {
            return Error.NotFound(description: "Album not found.");
        }

        return new AlbumModel(album);
    }

    public async Task<ErrorOr<List<AlbumModel>>> GetAllAsync() =>
        await dbContext.Albums.AsNoTracking()
                               .Include(x => x.Artist)
                               .Include(x => x.Songs)
                               .Select(x => new AlbumModel(x))
                               .ToListAsync();

    public async Task<ErrorOr<PaginationModel<AlbumModel>>> GetPagedAsync(int page = 0)
    {
        page = page <= 0 ? 1 : page - 1;

        var motorcycles = await dbContext.Albums.AsNoTracking()
                                                     .Include(x => x.Artist)
                                                     .Include(x => x.Songs)
                                                     .Skip(page * ROW_COUNT)
                                                     .Take(ROW_COUNT)
                                                     .Select(x => new AlbumModel(x))
                                                     .ToListAsync();

        var paginationModel = new PaginationModel<AlbumModel>
        {
            Items = motorcycles,
            Count = await dbContext.Albums.CountAsync()
        };

        return paginationModel;
    }
}
