namespace Solution.Core.Interfaces;

public interface ISongService
{
    Task<ErrorOr<List<SongModel>>> GetAllAsync();
    Task<ErrorOr<SongModel>> GetByIdAsync(int id);
    Task<ErrorOr<SongModel>> CreateAsync(SongModel model);
    Task<ErrorOr<Success>> UpdateAsync(SongModel model);
    Task<ErrorOr<Success>> DeleteAsync(int id);
    Task<ErrorOr<PaginationModel<SongModel>>> GetPagedAsync(int page);
}
