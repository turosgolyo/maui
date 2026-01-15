using Authentification.Domain.Models.Views;
using System.ComponentModel;

namespace Authentification.WebAPI.Controllers;

[ApiController]
[ProducesResponseType(statusCode: 400, type: typeof(BadRequestObjectResult))]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Route("api/users")]
    [Authorize]
    [ProducesResponseType(statusCode: 200, type: typeof(ICollection<UserModel>))]
    [EndpointDescription("This endpoint will return all users from the database")]
    public async Task<IActionResult> GetUserAsync()
    {
        var result = await userService.GetAllUsers();
        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemResult()
        );
    }
}
