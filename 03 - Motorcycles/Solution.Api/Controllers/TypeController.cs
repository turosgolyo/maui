using Solution.Services;

namespace Solution.Api.Controllers;

public class TypeController(ITypeService typeService) : BaseController
{
    //get all
    [HttpGet]
    [Route("api/types/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await typeService.GetAllAsync();
        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    //get by id
    [HttpGet]
    [Route("api/types/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        var result = await typeService.GetByIdAsync(id);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    //get paged
    [HttpGet]
    [Route("api/types/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute][Required] int page)
    {
        var result = await typeService.GetPagedAsync(page);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //create
    [HttpPost]
    [Route("api/types/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] TypeModel type)
    {
        var result = await typeService.CreateAsync(type);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    //update
    [HttpPut]
    [Route("api/types/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] TypeModel type)
    {
        var result = await typeService.UpdateAsync(type);
        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors));
    }

    //delete
    [HttpDelete]
    [Route("api/types/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
    {
        var result = await typeService.DeleteAsync(id);
        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors));
    }
}
