using TaskManagement.Model.Entity;

namespace TaskManagement.Service
{
    public class AssignReviwer
    {
        public List<TaskManage> AssignReviewersToTask(List<TaskManage> tasks)
        {
            var taskInReview = tasks.Where(t => t.reviewerName is null && t.status == "in-review").ToList();

            var teamMembers = tasks.Select(t => t.asigneeName).Distinct().ToList();

            var reviewEffort = new Dictionary<string, int>();

            foreach (var member in teamMembers)
            {
                reviewEffort[member] = tasks
                    .Where(t => t.reviewerName  == member && t.status == "in-review")
                    .Sum(t => t.estimatelnHours);
            }

            foreach (var task in taskInReview)
            {
                var potentialReviewer = teamMembers
                    .Where(m => m != task.asigneeName).ToList();

                var reviewer = potentialReviewer
                    .OrderBy(r => reviewEffort[r]).First();

                task.reviewerName = reviewer;

                reviewEffort[reviewer] += task.estimatelnHours;
            }

            return tasks;
        }
    }
}
