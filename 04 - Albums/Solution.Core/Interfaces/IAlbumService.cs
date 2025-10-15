namespace Solution.Core.Interfaces;

public interface IAlbumService
{
    Task<ErrorOr<List<AlbumModel>>> GetAllAsync();
    Task<ErrorOr<AlbumModel>> GetByIdAsync(int id);
    Task<ErrorOr<AlbumModel>> CreateAsync(AlbumModel model);
    Task<ErrorOr<Success>> UpdateAsync(AlbumModel model);
    Task<ErrorOr<Success>> DeleteAsync(int id);
    Task<ErrorOr<PaginationModel<AlbumModel>>> GetPagedAsync(int page);
}
