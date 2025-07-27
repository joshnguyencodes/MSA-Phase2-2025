using bulkbuy.api.Models;
using bulkbuy.api.Models.DTOs;
using bulkbuy.api.Repositories;

namespace bulkbuy.api.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<GroupResponse> CreateGroupAsync(CreateGroupRequest request, int creatorId)
        {
            var group = new Group
            {
                Name = request.Name,
                Description = request.Description,
                Theme = request.Theme,
                OwnerId = creatorId,
                CreatedAt = DateTime.UtcNow
            };

            var createdGroup = await _groupRepository.CreateAsync(group);

            // Add creator as a member with Creator role
            var creatorMembership = new GroupMember
            {
                UserId = creatorId,
                GroupId = createdGroup.Id,
                Role = GroupRole.Creator,
                JoinedAt = DateTime.UtcNow
            };

            await _groupRepository.AddMemberAsync(creatorMembership);

            // Reload group with creator information
            var groupWithCreator = await _groupRepository.GetByIdAsync(createdGroup.Id);

            return new GroupResponse
            {
                Id = groupWithCreator!.Id,
                Name = groupWithCreator.Name,
                Description = groupWithCreator.Description,
                Theme = groupWithCreator.Theme,
                CreatedAt = groupWithCreator.CreatedAt,
                Creator = new UserResponse
                {
                    Id = groupWithCreator.Owner.Id,
                    Username = groupWithCreator.Owner.Username
                },
                MemberCount = 1,
                IsUserMember = true
            };
        }

        public async Task<List<GroupResponse>> GetAllGroupsAsync(int currentUserId)
        {
            var groups = await _groupRepository.GetAllAsync();
            var groupResponses = new List<GroupResponse>();

            foreach (var group in groups)
            {
                var isUserMember = await _groupRepository.IsUserMemberAsync(currentUserId, group.Id);
                groupResponses.Add(new GroupResponse
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    Theme = group.Theme,
                    CreatedAt = group.CreatedAt,
                    Creator = new UserResponse
                    {
                        Id = group.Owner.Id,
                        Username = group.Owner.Username
                    },
                    MemberCount = group.Members.Count,
                    IsUserMember = isUserMember
                });
            }

            return groupResponses;
        }

        public async Task<List<GroupResponse>> GetUserGroupsAsync(int userId)
        {
            var groups = await _groupRepository.GetUserGroupsAsync(userId);
            return groups.Select(group => new GroupResponse
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Theme = group.Theme,
                CreatedAt = group.CreatedAt,
                Creator = new UserResponse
                {
                    Id = group.Owner.Id,
                    Username = group.Owner.Username
                },
                MemberCount = group.Members.Count,
                IsUserMember = true
            }).ToList();
        }

        public async Task<GroupDetailResponse?> GetGroupByIdAsync(int id, int currentUserId)
        {
            var group = await _groupRepository.GetByIdWithMembersAsync(id);
            if (group == null) return null;

            var userMembership = await _groupRepository.GetMembershipAsync(currentUserId, id);
            var isUserMember = userMembership != null;

            return new GroupDetailResponse
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Theme = group.Theme,
                CreatedAt = group.CreatedAt,
                Creator = new UserResponse
                {
                    Id = group.Owner.Id,
                    Username = group.Owner.Username
                },
                Members = group.Members.Select(m => new GroupMemberResponse
                {
                    User = new UserResponse
                    {
                        Id = m.User.Id,
                        Username = m.User.Username
                    },
                    JoinedAt = m.JoinedAt,
                    Role = m.Role
                }).ToList(),
                Orders = group.Orders.Select(o => new OrderResponse
                {
                    Id = o.Id,
                    Name = o.Name,
                    TargetAmount = o.TargetAmount,
                    CreatedAt = o.CreatedAt,
                    Status = o.Status
                }).ToList(),
                IsUserMember = isUserMember,
                UserRole = userMembership?.Role
            };
        }

        public async Task<bool> JoinGroupAsync(int groupId, int userId)
        {
            // Check if user is already a member
            var existingMembership = await _groupRepository.GetMembershipAsync(userId, groupId);
            if (existingMembership != null) return false;

            // Check if group exists
            var group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null) return false;

            var newMembership = new GroupMember
            {
                UserId = userId,
                GroupId = groupId,
                Role = GroupRole.Member,
                JoinedAt = DateTime.UtcNow
            };

            await _groupRepository.AddMemberAsync(newMembership);
            return true;
        }

        public async Task<bool> LeaveGroupAsync(int groupId, int userId)
        {
            var membership = await _groupRepository.GetMembershipAsync(userId, groupId);
            if (membership == null) return false;

            // Prevent creator from leaving their own group
            if (membership.Role == GroupRole.Creator) return false;

            await _groupRepository.RemoveMemberAsync(userId, groupId);
            return true;
        }

        public async Task<bool> DeleteGroupAsync(int groupId, int userId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null || group.OwnerId != userId) return false;

            await _groupRepository.DeleteAsync(groupId);
            return true;
        }
    }
}
