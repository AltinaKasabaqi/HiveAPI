namespace HiveAPI.Models
{
    public class List
    {
        public int ListId { get; set; }
        public string ListName { get; set; }

        public int WorkSpaceId { get; set; }
        public WorkSpace WorkSpace { get; set; }
    }
}
