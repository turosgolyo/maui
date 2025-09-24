
namespace Solution.Core.Interfaces;

public interface IManufacturerService
{
    Task<ErrorOr<List<ManufacturerModel>>> GetAllAsync();
    Task<ErrorOr<ManufacturerModel>> GetByIdAsync(int id);
    Task<ErrorOr<ManufacturerModel>> CreateAsync(ManufacturerModel model);
    Task<ErrorOr<Success>> UpdateAsync(ManufacturerModel model);
    Task<ErrorOr<Success>> DeleteAsync(int id);
}
