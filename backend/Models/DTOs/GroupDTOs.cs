using System.ComponentModel.DataAnnotations;

namespace bulkbuy.api.Models.DTOs
{
    public class CreateGroupRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public string Theme { get; set; }
    }
    
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Theme { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserResponse Creator { get; set; }
        public int MemberCount { get; set; }
        public bool IsUserMember { get; set; }
    }
    
    public class GroupDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Theme { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserResponse Creator { get; set; }
        public List<GroupMemberResponse> Members { get; set; }
        public List<OrderResponse> Orders { get; set; }
        public bool IsUserMember { get; set; }
        public GroupRole? UserRole { get; set; }
    }
    
    public class GroupMemberResponse
    {
        public int Id { get; set; }
        public UserResponse User { get; set; }
        public DateTime JoinedAt { get; set; }
        public GroupRole Role { get; set; }
    }
    
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
    
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TargetAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
    }
}
