
using ErrorOr;
using Solution.Core.Models;

namespace Solution.Core.Interfaces;

public interface IRunningService
{
    Task<ErrorOr<RunningModel>> CreateAsync(RunningModel run);
}
