using HiveAPI.Modals;
using HiveAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HiveAPI
{
    public class APIDbContext:  DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { 
        
        }
                
        public DbSet<User> Users { get; set; }

        public DbSet<WorkSpace> WorkSpaces { get; set; }

        public DbSet<Collaborator> Collaborators { get; set; }

        public DbSet<Models.Task> Tasks { get; set; }
    }


}
