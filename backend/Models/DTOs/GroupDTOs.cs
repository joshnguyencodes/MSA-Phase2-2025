using System.ComponentModel.DataAnnotations;

namespace bulkbuy.api.Models.DTOs
{
    public class CreateGroupRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public string Theme { get; set; } = string.Empty;
    }
    
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Theme { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserResponse Creator { get; set; } = new UserResponse();
        public int MemberCount { get; set; }
        public bool IsUserMember { get; set; }
    }
    
    public class GroupDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Theme { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserResponse Creator { get; set; } = new UserResponse();
        public List<GroupMemberResponse> Members { get; set; } = new List<GroupMemberResponse>();
        public List<OrderResponse> Orders { get; set; } = new List<OrderResponse>();
        public bool IsUserMember { get; set; }
        public GroupRole? UserRole { get; set; }
    }
    
    public class GroupMemberResponse
    {
        public UserResponse User { get; set; } = new UserResponse();
        public DateTime JoinedAt { get; set; }
        public GroupRole Role { get; set; }
    }
    
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
    }
    
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TargetAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
    }
}
