using Microsoft.AspNetCore.Mvc;
using Solution.Core.Interfaces;

namespace Solution.Api.Controllers;

public class MotorcycleController(IMotorcycleService motorcycleService) : ControllerBase
{
    [HttpGet]
    [Route("api/motorcycles")]
    public async Task <IActionResult> GetAlldAsync()
    {
        return Ok(await motorcycleService.GetAllAsync());
    }
}

