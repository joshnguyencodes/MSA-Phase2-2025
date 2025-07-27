using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace bulkbuy.api.Models
{
    public class GroupMember
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [Required]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; } = null!;

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
