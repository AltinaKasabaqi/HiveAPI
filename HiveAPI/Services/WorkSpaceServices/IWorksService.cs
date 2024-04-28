using HiveAPI.Models;
using System.Threading.Tasks;

namespace HiveAPI.Services.WorkSpaceServices
{
    public interface IWorksService
    {
        Task<List<WorkSpace>> GetWorkSpaces();
        Task<WorkSpace> GetWorkSpaceById(int id);

        Task<List<WorkSpace>> GetWorkSpacesByUserId(int wId);

        Task<int> CreateWorkSpace(WorkSpace ws);

        System.Threading.Tasks.Task UpdateWorkSpace(int id, WorkSpace workSpace);

        System.Threading.Tasks.Task DeleteWorkSpace(int id);
    }
}
