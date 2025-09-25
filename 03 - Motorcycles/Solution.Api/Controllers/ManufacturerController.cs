namespace Solution.Api.Controllers;

public class ManufacturerController(IManufacturerService manufacturerService) : BaseController
{
    //get all
    [HttpGet]
    [Route("api/manufacturers/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await manufacturerService.GetAllAsync();
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //get by id
    [HttpGet]
    [Route("api/manufacturers/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        var result = await manufacturerService.GetByIdAsync(id);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //get paged
    [HttpGet]
    [Route("api/manufacturers/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute][Required] int page)
    {
        var result = await manufacturerService.GetPagedAsync(page);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //create
    [HttpPost]
    [Route("api/manufacturers/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] ManufacturerModel manufacturer)
    {
        var result = await manufacturerService.CreateAsync(manufacturer);
        
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
            );
    }

    //update
    [HttpPut]
    [Route("api/manufacturers/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] ManufacturerModel manufacturer)
    {
        var result = await manufacturerService.UpdateAsync(manufacturer);
        
        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }

    //delete
    [HttpDelete]
    [Route("api/manufacturers/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
    {
        var result = await manufacturerService.DeleteAsync(id);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }
}
