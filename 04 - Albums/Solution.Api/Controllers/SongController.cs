namespace Solution.Api.Controllers;

public class SongController(ISongService songService) : BaseController
{

    // GET ALL
    [HttpGet]
    [Route("api/songs/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await songService.GetAllAsync();
        return result.Match(
            artists => Ok(artists),
            errors => Problem(errors)
        );
    }
    //get by id
    [HttpGet]
    [Route("api/songs/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        var result = await songService.GetByIdAsync(id);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //get paged
    [HttpGet]
    [Route("api/songs/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute][Required] int page)
    {
        var result = await songService.GetPagedAsync(page);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    //create
    [HttpPost]
    [Route("api/songs/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] SongModel song)
    {
        var result = await songService.CreateAsync(song);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
            );
    }

    //update
    [HttpPut]
    [Route("api/songs/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] SongModel song)
    {
        var result = await songService.UpdateAsync(song);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }

    //delete
    [HttpDelete]
    [Route("api/songs/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
    {
        var result = await songService.DeleteAsync(id);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
            );
    }
}
