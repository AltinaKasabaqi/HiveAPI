namespace HiveAPI.Models
{
    public class Collaborator
    {
        public int CollaboratorId { get; set; }

        // Reference to the User's email
        public string? UserEmail { get; set; }

        // Reference to the WorkSpace
        public int WorkSpaceId { get; set; }
        public WorkSpace WorkSpace { get; set; }
    }
}
