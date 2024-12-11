namespace Solution.Services;

public class MovieService(AppDbContext dbContext) : IMovieInterface
{
    public async Task CreateAsync(MovieModel movie)
    {
        MovieEntity entity = movie.ToEntity();
        entity.PublicId = Guid.NewGuid().ToString();

        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }
}
