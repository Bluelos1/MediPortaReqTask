using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class Collective
{
    [JsonIgnore]
    [Key]
    public int CollectiveId { get; set; }
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }
    [JsonPropertyName("external_links")]
    public List<ExternalLink> ExternalLinks { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
}