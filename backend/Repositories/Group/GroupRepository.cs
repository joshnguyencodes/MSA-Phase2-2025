using bulkbuy.api.Data;
using bulkbuy.api.Models;
using Microsoft.EntityFrameworkCore;

namespace bulkbuy.api.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _context.Groups
                .Include(g => g.Owner)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Group?> GetByIdWithMembersAsync(int id)
        {
            return await _context.Groups
                .Include(g => g.Owner)
                .Include(g => g.Members)
                    .ThenInclude(m => m.User)
                .Include(g => g.Orders)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _context.Groups
                .Include(g => g.Owner)
                .Include(g => g.Members)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Group>> GetUserGroupsAsync(int userId)
        {
            return await _context.Groups
                .Include(g => g.Owner)
                .Include(g => g.Members)
                .Where(g => g.Members.Any(m => m.UserId == userId))
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }

        public async Task<Group> CreateAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task DeleteAsync(int id)
        {
            var group = await GetByIdAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GroupMember?> GetMembershipAsync(int userId, int groupId)
        {
            return await _context.GroupMembers
                .Include(gm => gm.User)
                .Include(gm => gm.Group)
                .FirstOrDefaultAsync(gm => gm.UserId == userId && gm.GroupId == groupId);
        }

        public async Task<GroupMember> AddMemberAsync(GroupMember member)
        {
            _context.GroupMembers.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task RemoveMemberAsync(int userId, int groupId)
        {
            var member = await GetMembershipAsync(userId, groupId);
            if (member != null)
            {
                _context.GroupMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsUserMemberAsync(int userId, int groupId)
        {
            return await _context.GroupMembers
                .AnyAsync(gm => gm.UserId == userId && gm.GroupId == groupId);
        }
    }
}
