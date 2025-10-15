namespace Solution.Core.Interfaces;

public interface IArtistService
{
    Task<ErrorOr<List<ArtistModel>>> GetAllAsync();
    Task<ErrorOr<ArtistModel>> GetByIdAsync(int id);
    Task<ErrorOr<ArtistModel>> CreateAsync(ArtistModel model);
    Task<ErrorOr<Success>> UpdateAsync(ArtistModel model);
    Task<ErrorOr<Success>> DeleteAsync(int id);
    Task<ErrorOr<PaginationModel<ArtistModel>>> GetPagedAsync(int page);
}
