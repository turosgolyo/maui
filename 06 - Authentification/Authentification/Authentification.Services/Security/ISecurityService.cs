namespace Authentification.Services.Security;

public interface ISecurityService
{
    Task<ErrorOr<TokenResponseModel>> LoginAsnyc(LoginRequestModel model);
    Task<ErrorOr<Success>> RegisterAsnyc(RegisterRequestModel model);
}
