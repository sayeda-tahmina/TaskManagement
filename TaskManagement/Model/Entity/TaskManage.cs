using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Model.Entity
{
    public class TaskManage
    {
        [Key]
        public int taskId { get; set; }
        public string asigneeName { get; set; }
        public string? reviewerName { get; set; }
        public string status { get; set; }
        public int estimatelnHours { get; set; }
    }
}
