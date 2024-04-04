using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediportaTask.ContextDb;
using MediportaTask.Model;
using MediportaTask.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace MediportaTask.Tests.TagFetcher;

[TestSubject(typeof(TagsFetcher))]
public class TagsFetcherTest
{
    [Fact]
    public async Task FetchTagsFromApi_ReturnsCorrectData_OnSuccess()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<TagDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        using (var context = new TagDbContext(options))
        {
            var mockHttpClientHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"items\": [{\"name\": \"C#\", \"count\": 100}]}"),
            };

            mockHttpClientHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpClientHandler.Object);
            var mockLogger = new Mock<ILogger<TagsFetcher>>();
            var tagsFetcher = new TagsFetcher(httpClient, context, mockLogger.Object);


            // Act
            var result = await tagsFetcher.FetchTagsFromApi(1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal("C#", result.Items.First().Name);
        }
    }
}
