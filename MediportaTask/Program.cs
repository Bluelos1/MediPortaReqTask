using MediportaTask.ContextDb;
using MediportaTask.Interface;
using MediportaTask.Service;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TagDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<ITagsFetcher,TagsFetcher>()
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
    });

builder.Services.AddScoped<IStackOverflowService, StackOverflowService>();
builder.Services.AddLogging(configure => configure.AddConsole())
    .AddTransient<TagsFetcher>()
    .AddTransient<StackOverflowService>();
builder.Services.AddHostedService<FetcherInitializator>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();
app.Run();
public partial class Program { }


