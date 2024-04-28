
using HiveAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HiveAPI.Services.ListServices
{
    public class ListService : IListService
    {
        public ListService(APIDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        private readonly APIDbContext _context;

        public async Task<List<HiveAPI.Models.List>> GetLists()
        {
            var lists = await _context.Lists
                .Include(l => l.WorkSpace)
                .ToListAsync();

            return lists;
        }


        public async Task<int> CreateList(Models.List list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            // Sigurohu që ekziston hapësira e punës për listën
            var workspace = await _context.WorkSpaces.FirstOrDefaultAsync(ws => ws.WId == list.WorkSpaceId);
            if (workspace == null)
            {
                throw new ArgumentException("Workspace not found", nameof(list));
            }

            // Shto listën në kontekstin e bazës së të dhënave dhe ruaje ndryshimet
            list.WorkSpace = workspace;
            _context.Set<List>().Add(list);
            await _context.SaveChangesAsync();

            // Kthe identifikuesin e listës së krijuar
            return list.ListId;
        }

        public async System.Threading.Tasks.Task DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                throw new ArgumentException("List not found", nameof(id));
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();

            
        }

        public async Task<List> GetListById(int id)
        {
            var list = await _context.Lists
                .Include(l => l.WorkSpace)
                .Where(l => l.ListId == id)
                .Select(l => new List
                {
                    ListId = l.ListId,
                    ListName = l.ListName,
                    WorkSpace = new WorkSpace
                    {
                        WId = l.WorkSpace.WId,
                        WorkspaceName = l.WorkSpace.WorkspaceName,
                        WorkspaceDescription = l.WorkSpace.WorkspaceDescription
                    }
                })
                .FirstOrDefaultAsync();

            return list;
        }

       

        public async Task<List<List>> GetListsByWorkspaceId(int wId)
        {
            var lists = await _context.Lists
                .Include(l => l.WorkSpace)
                .Where(l => l.WorkSpaceId == wId)
                .Select(l => new List
                {
                    ListId = l.ListId,
                    ListName = l.ListName,
                    WorkSpace = new WorkSpace
                    {
                        WId = l.WorkSpace.WId,
                        WorkspaceName = l.WorkSpace.WorkspaceName
                    }
                })
                .ToListAsync();

            return lists;
        }

        public async System.Threading.Tasks.Task UpdateList(int id, List list)
        {
            var existingList = await _context.Lists.FirstOrDefaultAsync(x => x.ListId == id);
            if (existingList == null)
            {
                throw new ArgumentException("List not found", nameof(id));
            }

            existingList.ListName = list.ListName;
            // Update other properties as needed

            await _context.SaveChangesAsync();
        }
    }
}
