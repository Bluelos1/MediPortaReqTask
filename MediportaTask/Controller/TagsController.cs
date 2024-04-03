using MediportaTask.Interface;
using MediportaTask.Model;
using Microsoft.AspNetCore.Mvc;

namespace MediportaTask.Controller;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly IStackOverflowService _overflowService;
    private readonly ITagsFetcher _tagsFetcher;

    public TagsController(IStackOverflowService overflowService, ITagsFetcher tagsFetcher)
    {
        _overflowService = overflowService;
        _tagsFetcher = tagsFetcher;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTagFromStackOverflow([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortField = "count", [FromQuery] string sortOrder = "desc")
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and PageSize should be greater than zero.");
        }

        if (sortOrder != "asc" && sortOrder != "desc")
        {
            return BadRequest("SortOrder must be either 'asc' or 'desc'.");
        }

        if (sortField != "name" && sortField != "count")
        {
            return BadRequest("SortField must be either 'name' or 'count'.");
        }
        var tags = await _overflowService.GetTagsAsync(page, pageSize, sortOrder,sortField);
        return Ok(tags);
    }
    
    [HttpGet("refresh")]
    public async Task<ActionResult> RefreshTags()
    {
        await _tagsFetcher.InitializeTags();
        return Ok("Tags refreshed successfully.");
    }
}