namespace Solution.Api.Controllers;

public class AlbumController(IAlbumService albumService) : BaseController
{

    // GET ALL
    [HttpGet]
    [Route("api/albums/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await albumService.GetAllAsync();
        return result.Match(
            artists => Ok(artists),
            errors => Problem(errors)
        );
    }
    //get by id
    [HttpGet]
    [Route("api/albums/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        var result = await albumService.GetByIdAsync(id);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //get paged
    [HttpGet]
    [Route("api/albums/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute][Required] int page)
    {
        var result = await albumService.GetPagedAsync(page);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //create
    [HttpPost]
    [Route("api/albums/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] AlbumModel album)
    {
        var result = await albumService.CreateAsync(album);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
            );
    }

    //update
    [HttpPut]
    [Route("api/albums/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] AlbumModel album)
    {
        var result = await albumService.UpdateAsync(album);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }

    //delete
    [HttpDelete]
    [Route("api/albums/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
    {
        var result = await albumService.DeleteAsync(id);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }
}
