using MediportaTask.ContextDb;
using MediportaTask.Interface;
using MediportaTask.Model;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Service;



public class StackOverflowService : IStackOverflowService
{
    private readonly TagDbContext _context;
    private readonly ILogger<StackOverflowService> _logger;

    public StackOverflowService(TagDbContext context, ILogger<StackOverflowService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync(int page, int pageSize, string sortOrder, string sortField)
    {
        _logger.LogInformation("Receive tags: page {Page}, page size{PageSize}, sorting order {SortOrder}, sorting field{SortField}.", page, pageSize, sortOrder, sortField);
        try
        {
            IQueryable<Tag> sortedTags = _context.Tags;
            sortedTags = sortField switch
            {
                "name" => sortOrder == "desc"
                    ? sortedTags.OrderByDescending(t => t.name)
                    : sortedTags.OrderBy(t => t.name),
                "count" => sortOrder == "desc"
                    ? sortedTags.OrderByDescending(t => t.count)
                    : sortedTags.OrderBy(t => t.count),
                _ => sortedTags
            };
            var tags = await sortedTags.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            _logger.LogInformation("Successfully received  {TagsCount} tags.", tags.Count);
            return tags;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while receiving tags: page {Page}, page size {PageSize}.", page, pageSize);
            throw;
        }
    }
}