namespace bulkbuy.api.Models
{
    public class GroupMember
    {

        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        
        [Required]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
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
