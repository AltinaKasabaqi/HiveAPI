
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HiveAPI.Models;
namespace HiveAPI.Services.TaskServices
{
    public class TaskService : ITaskService
    {
        private readonly APIDbContext _context;

        public TaskService(APIDbContext context)
        {
            _context = context;
        }


        

        public async Task<int> AddTask(Models.Task task)
        {
            if (task == null)
            {
                throw new ArgumentException("List not found");
            }

            var list = await _context.Lists.FirstOrDefaultAsync(l => l.ListId == task.TaskId);

            task.List = list;

            var addedTask = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return addedTask.Entity.TaskId;
        }

        public async System.Threading.Tasks.Task DeleteTask(int id)
        {
            
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    throw new ArgumentException("Task not found");
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasks()
        {
            return await _context.Tasks
                .Include(t => t.List)
                .ToListAsync();
        }

        public async Task<Models.Task> GetTaskById(int id)
        {
            return await _context.Tasks
                .Include(t => t.List)
                .FirstOrDefaultAsync(t => t.TaskId == id);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByListId(int listId)
        {
            return await _context.Tasks
                .Include(t => t.List)
                .Where(t => t.ListId == listId)
                .ToListAsync();
        }

       

        public async System.Threading.Tasks.Task UpdateTask(int id, Models.Task task)
        {
            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null)
            {
                throw new ArgumentException("Task not found");
            }

            _context.Entry(existingTask).CurrentValues.SetValues(task);

            
                await _context.SaveChangesAsync();
           
        }
    }
}
