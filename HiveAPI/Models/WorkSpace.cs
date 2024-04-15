using HiveAPI.Modals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiveAPI.Models
{
    public class WorkSpace
    {

        [Key]
        public int WId { get; set; }
        public string? WorkspaceName { get; set;}
        public string? WorkspaceDescription { get; set;}

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }


    }
}
