using bulkbuy.api.Models;
using bulkbuy.api.Models.DTOs;

namespace bulkbuy.api.Services
{
    public interface IGroupService
    {
        Task<GroupResponse> CreateGroupAsync(CreateGroupRequest request, int creatorId);
        Task<List<GroupResponse>> GetAllGroupsAsync(int currentUserId);
        Task<List<GroupResponse>> GetUserGroupsAsync(int userId);
        Task<GroupDetailResponse?> GetGroupByIdAsync(int id, int currentUserId);
        Task<bool> JoinGroupAsync(int groupId, int userId);
        Task<bool> LeaveGroupAsync(int groupId, int userId);
        Task<bool> DeleteGroupAsync(int groupId, int userId);
    }
}
