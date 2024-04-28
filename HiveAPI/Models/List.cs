using System.ComponentModel.DataAnnotations.Schema;

namespace HiveAPI.Models
{
    [Table ("List")]
    public class List
    {
        public int ListId { get; set; }
        public string ListName { get; set; }

        public int WorkSpaceId { get; set; }
        public WorkSpace WorkSpace { get; set; }
    }
}
