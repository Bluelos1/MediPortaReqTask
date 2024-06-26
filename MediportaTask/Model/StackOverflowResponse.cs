using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class StackOverflowResponse
{
    [JsonIgnore]
    [Key]
    public int ResponseId { get; set; }
    [JsonPropertyName("items")]
    public List<Tag> Items { get; set; }
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
    [JsonPropertyName("quota_max")]
    public int QuotaMax { get; set; }
    [JsonPropertyName("quota_remaining")]
    public int QuotaRemaining { get; set; }
}