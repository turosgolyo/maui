namespace Solution.Api.Controllers;

public class ArtistController(IArtistService artistService) : BaseController
{

    // GET ALL
    [HttpGet]
    [Route("api/artists/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await artistService.GetAllAsync();
        return result.Match(
            artists => Ok(artists),
            errors => Problem(errors)
        );
    }
    //get by id
    [HttpGet]
    [Route("api/artists/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        var result = await artistService.GetByIdAsync(id);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //get paged
    [HttpGet]
    [Route("api/artists/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute][Required] int page)
    {
        var result = await artistService.GetPagedAsync(page);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //create
    [HttpPost]
    [Route("api/artists/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] ArtistModel artist)
    {
        var result = await artistService.CreateAsync(artist);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
            );
    }

    //update
    [HttpPut]
    [Route("api/artists/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] ArtistModel manufacturer)
    {
        var result = await artistService.UpdateAsync(manufacturer);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }

    //delete
    [HttpDelete]
    [Route("api/artists/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
    {
        var result = await artistService.DeleteAsync(id);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }
}
