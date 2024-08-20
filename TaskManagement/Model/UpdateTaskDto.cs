namespace TaskManagement.Model
{
    public class UpdateTaskDto
    {
        public string asigneeName { get; set; }
        public string? reviewerName { get; set; }
        public string status { get; set; }
        public int estimatelnHours { get; set; }
    }
}
