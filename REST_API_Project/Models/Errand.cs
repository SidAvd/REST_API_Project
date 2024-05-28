using System.ComponentModel.DataAnnotations;

namespace REST_API_Project.Models
{
    public class Errand
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
        public string? Description { get; set; }
        public ICollection<Worker>? Workers { get; set; }
    }
}
