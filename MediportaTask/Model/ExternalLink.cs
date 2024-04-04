
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class ExternalLink
{
    [JsonIgnore]
    [Key]
    public int LinkId { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
}