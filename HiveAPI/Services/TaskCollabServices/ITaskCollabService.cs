using HiveAPI.Models;

namespace HiveAPI.Services.TaskCollabServices
{
    public interface ITaskCollabService
    {
        System.Threading.Tasks.Task<TaskCollab> GetTaskCollabById(int id);
        System.Threading.Tasks.Task<int> AddTaskCollab(TaskCollab taskCollab);
        System.Threading.Tasks.Task UpdateTaskCollab(int id, TaskCollab taskCollab);
        System.Threading.Tasks.Task DeleteTaskCollab(int id);
    }
}
