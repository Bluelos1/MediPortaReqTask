using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MediportaTask.Model;
public class StackOverflowResponse
{
    [JsonIgnore]
    [Key]
    public int ResponseId { get; set; }
    public List<Tag> items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }

}