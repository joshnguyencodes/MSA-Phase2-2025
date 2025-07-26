using bulkbuy.api.Models;

namespace bulkbuy.api.Repositories
{
    public interface IGroupRepository
    {
        Task<Group?> GetByIdAsync(int id);
        Task<Group?> GetByIdWithMembersAsync(int id);
        Task<List<Group>> GetAllAsync();
        Task<List<Group>> GetUserGroupsAsync(int userId);
        Task<Group> CreateAsync(Group group);
        Task<Group> UpdateAsync(Group group);
        Task DeleteAsync(int id);
        Task<GroupMember?> GetMembershipAsync(int userId, int groupId);
        Task<GroupMember> AddMemberAsync(GroupMember member);
        Task RemoveMemberAsync(int userId, int groupId);
        Task<bool> IsUserMemberAsync(int userId, int groupId);
    }
}
