namespace REST_API_Project.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly? HireDate { get; set; }
        public ICollection<ErrandWorker>? ErrandWorkers { get; set; }
    }
}
