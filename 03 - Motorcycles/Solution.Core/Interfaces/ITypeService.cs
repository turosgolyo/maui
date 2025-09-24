
namespace Solution.Core.Interfaces;

public interface ITypeService
{
    Task<ErrorOr<List<TypeModel>>> GetAllAsync();
    Task<ErrorOr<TypeModel>> GetByIdAsync(int id);
    Task<ErrorOr<TypeModel>> CreateAsync(TypeModel model);
    Task<ErrorOr<Success>> UpdateAsync(TypeModel model);
    Task<ErrorOr<Success>> DeleteAsync(int id);
}
