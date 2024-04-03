using MediportaTask.Model;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.ContextDb;

public class TagDbContext : DbContext
{
    public TagDbContext(DbContextOptions<TagDbContext> options) : base(options) { }
    
    public DbSet<Tag> Tags { get; set; }
    public DbSet<StackOverflowResponse> StackOverflowResponses { get; set; }
    public DbSet<Collective> Collectives { get; set; }
    public DbSet<ExternalLink> ExternalLinks { get; set; }

}