
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class ExternalLink
{
    [JsonIgnore]
    [Key]
    public int LinkId { get; set; }
    public string type { get; set; }
    public string link { get; set; }
}