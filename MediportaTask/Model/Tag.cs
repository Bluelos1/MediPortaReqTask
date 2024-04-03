using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class Tag
{
    [JsonIgnore]
    [Key]
    public int TagId { get; set; }
    public bool has_synonyms { get; set; }
    public bool is_moderator_only { get; set; }
    public bool is_required { get; set; }
    public int count { get; set; }
    public string name { get; set; }
    public List<Collective> collectives { get; set; }
    public double percentage { get; set; }
}