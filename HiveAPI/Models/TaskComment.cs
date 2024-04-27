using System.ComponentModel.DataAnnotations;

namespace HiveAPI.Models
{
    public class TaskComment
    {
        [Key]
        public int TaskCommentsId { get; set; }

        // Reference to the User's email
        public string? UserEmail { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public string? Comment  { get; set; }
    }
}
