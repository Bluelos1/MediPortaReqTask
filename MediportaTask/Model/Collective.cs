using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class Collective
{
    [JsonIgnore]
    [Key]
    public int CollectiveId { get; set; }
    public List<string> tags { get; set; }
    public List<ExternalLink> external_links { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
}