using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediportaTask.ContextDb;
using MediportaTask.Model;
using MediportaTask.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace MediportaTask.Tests.Service;

[TestSubject(typeof(StackOverflowService))]
public class StackOverflowServiceTest
{

    [Fact]
    public async Task GetTagsAsync_ReturnsCorrectNumberOfTags()
    {
        // Arrange
        var tagDb = new DbContextOptionsBuilder<TagDbContext>()
            .UseInMemoryDatabase("TagTestDb") 
            .Options;

        await using var context = new TagDbContext(tagDb);
        context.Tags.AddRange(new Tag { name = "Java" }, new Tag { name = "Python" });
        await context.SaveChangesAsync();
        
        var service = new StackOverflowService(context, Mock.Of<ILogger<StackOverflowService>>());

        // Act
        var result = await service.GetTagsAsync(1, 10, "asc", "name");

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Java", result.First().name);
    }
}