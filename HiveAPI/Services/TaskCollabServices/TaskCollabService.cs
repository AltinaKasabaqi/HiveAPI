using HiveAPI.Models;
namespace HiveAPI.Services.TaskCollabServices
{
    public class TaskCollabService : ITaskCollabService
    {

        private readonly APIDbContext _context;

        public TaskCollabService(APIDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<TaskCollab> GetTaskCollabById(int id)
        {
            return await _context.TaskColls.FindAsync(id);
        }

       

        public async System.Threading.Tasks.Task<int> AddTaskCollab(TaskCollab taskCollab)
        {
            _context.TaskColls.Add(taskCollab);
            await _context.SaveChangesAsync();
            return taskCollab.Id;
        }

        public async System.Threading.Tasks.Task UpdateTaskCollab(int id, TaskCollab taskCollab)
        {
            var existingTaskCollab = await _context.TaskColls.FindAsync(id);
            if (existingTaskCollab == null)
            {
                throw new ArgumentException("TaskCollab not found");
            }

            existingTaskCollab.email = taskCollab.email;

            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteTaskCollab(int id)
        {
            var taskCollab = await _context.TaskColls.FindAsync(id);
            if (taskCollab == null)
            {
                throw new ArgumentException("TaskCollab not found");
            }

            _context.TaskColls.Remove(taskCollab);
            await _context.SaveChangesAsync();
        }

        
        
    }
}
