using System.ComponentModel.DataAnnotations;

namespace REST_API_Project.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Errand>? Errands { get; set; }
    }
}
