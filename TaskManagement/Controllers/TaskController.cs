using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Model;
using TaskManagement.Model.Entity;
using TaskManagement.Service;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TaskController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllTask()
        {
            return Ok(dbContext.Tasks.ToList());
        }

        [HttpGet]
        [Route("{taskId:int}")]
        public IActionResult GetTaskByID(int taskId)
        {
            var task = dbContext.Tasks.Find(taskId);

            if(task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public IActionResult AddTask(AddTaskDto addTaskDto)
        {
            var task = new TaskManage()
            {
                asigneeName = addTaskDto.asigneeName,
                reviewerName = addTaskDto.reviewerName,
                status = addTaskDto.status,
                estimatelnHours = addTaskDto.estimatelnHours
            };

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            if (task.reviewerName == null && task.status == "in-review")
            {
                var assignReviwerService = new AssignReviwer();

                var tasks = dbContext.Tasks.ToList();

                var updateTasks = assignReviwerService.AssignReviewersToTask(tasks);

                var updatedTask = updateTasks.FirstOrDefault(t => t.taskId == task.taskId);

                if(updatedTask != null)
                {
                    task.reviewerName = updatedTask.reviewerName;
                }
            }

            dbContext.SaveChanges();

            return Ok(task);
        }

        [HttpDelete]
        [Route("{taskId:int}")]
        public IActionResult DeleteTask(int taskId)
        {
            var task = dbContext.Tasks.Find(taskId);

            if(task == null)
            {
                return NotFound();
            }

            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{taskId:int}")]
        public IActionResult UpdateTask(int taskId, UpdateTaskDto updatetaskDto)
        {
            var task = dbContext.Tasks.Find(taskId);

            if(task == null)
            {
                return NotFound();
            }

            task.asigneeName = updatetaskDto.asigneeName;
            task.reviewerName = updatetaskDto.reviewerName;
            task.status = updatetaskDto.status;
            task.estimatelnHours = updatetaskDto.estimatelnHours;

            dbContext.SaveChanges();

            return Ok(task);
        }

    }
}
