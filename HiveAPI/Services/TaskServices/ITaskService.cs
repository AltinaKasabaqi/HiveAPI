namespace HiveAPI.Services.TaskServices
{
    public interface ITaskService
    {
        Task<IEnumerable<Models.Task>> GetAllTasks();
        Task<Models.Task> GetTaskById(int id);
        Task<int> AddTask(Models.Task task);
        System.Threading.Tasks.Task UpdateTask(int id, Models.Task task);
        System.Threading.Tasks.Task DeleteTask(int id);
        Task<IEnumerable<Models.Task>> GetTasksByListId(int listId);
        System.Threading.Tasks.Task MoveTask(int id, int newListId);
    }
}
