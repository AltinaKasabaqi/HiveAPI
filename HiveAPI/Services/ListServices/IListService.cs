using HiveAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace HiveAPI.Services.ListServices
{
    public interface IListService
    {
        Task<int> CreateList(List list);
         Task<List> GetListById(int id);
        Task<List<List>> GetListsByWorkspaceId(int wId);
        Task<List<List>> GetLists();
        System.Threading.Tasks.Task UpdateList(int id, List list);
        System.Threading.Tasks.Task DeleteList(int id);
    }
}
