using System.ComponentModel.DataAnnotations.Schema;

namespace HiveAPI.Models
{
    [Table ("Tasks")]
    public class Task
    {
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Priority priority { get; set; }

        // Lidhja me List
        public int ListId { get; set; }
        public List List { get; set; }
    }
}
