namespace bulkbuy.api.Models
{
    public class GroupMember
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int GroupId { get; set; }
        public Group Group { get; set; }
        
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        
        public GroupRole Role { get; set; } = GroupRole.Member;
    }
    
    public enum GroupRole
    {
        Member,
        Admin,
        Creator
    }
}
