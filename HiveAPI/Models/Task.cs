namespace HiveAPI.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Label { get; set; }


        // Lidhja me Collaborator
        public ICollection<Collaborator> Collaborators { get; set; }

        // Lidhja me Workspace
        public int ListId { get; set; }
        public List list{ get; set; }
    }
}
