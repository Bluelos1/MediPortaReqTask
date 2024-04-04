using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class Tag
{
    [JsonIgnore]
    [Key]
    public int TagId { get; set; }
    [JsonPropertyName("has_synonyms")]
    public bool HasSynonyms { get; set; }
    [JsonPropertyName("is_moderator_only")]
    public bool IsModeratorOnly { get; set; }
    [JsonPropertyName("is_required")]
    public bool IsRequired { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("collectives")]
    public List<Collective> Collectives { get; set; }
    [JsonPropertyName("percentage")]
    public double Percentage { get; set; }
}