using MediportaTask.ContextDb;
using MediportaTask.Interface;
using MediportaTask.Model;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Service;

public class TagsFetcher : ITagsFetcher
{
    private readonly HttpClient _httpClient;
    private readonly TagDbContext _context;  
    private readonly ILogger<TagsFetcher> _logger;

    public TagsFetcher(HttpClient httpClient, TagDbContext context, ILogger<TagsFetcher> logger)
    {
        _httpClient = httpClient;
        _context = context;
        _logger = logger;
    }
    
    public async Task InitializeTags()
    {
        _logger.LogInformation("Start fetching tags");
        var hasMore = true;
        var page = 1;
        while (hasMore)
        {
            var apiResponse = await FetchTagsFromApi(page);
            if (apiResponse == null) 
            {
                _logger.LogError($"Failed to fetch tags for page {page}.");
                break; 
            }
            hasMore = await ProcessApiResponse(apiResponse);
            page++;
        }

        try
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation("Tags fetched and saved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving tags to database");
        }
    }

    public async Task<StackOverflowResponse?> FetchTagsFromApi(int page)
    {
        var url = $"https://api.stackexchange.com/2.3/tags?page={page}&site=stackoverflow";
        try
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Unsuccessfully fetching tags: {StatusCode}", response.StatusCode);
                return null;
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<StackOverflowResponse>(responseBody);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed fetching tags on page: {PageNumber}.", page);
            return null;
        }
    }

    private async Task<bool> ProcessApiResponse(StackOverflowResponse apiResponse)
    {
        if (apiResponse?.items == null || !apiResponse.items.Any())
        {
            _logger.LogInformation("No more tags to add");
            return false;
        }

        var totalCount = apiResponse.items.Sum(tag => tag.count);
        foreach (var tag in apiResponse.items)
        {
            var existingTag =
                await _context.Tags.FirstOrDefaultAsync(t => t.name == tag.name || t.count == tag.count);
            if (existingTag != null)
            {
                existingTag.count = tag.count;
                tag.percentage = ((double)tag.count / totalCount) * 100;
                _logger.LogInformation("Update existing tag: {TagName}", tag.name);
            }
            else
            {
                _context.Tags.Add(tag);
                _logger.LogInformation("Add new tag: {TagName}", tag.name);
            }
            
        }
        return apiResponse.items.Count < 1000;
    }
    
}