using Microsoft.AspNetCore.Mvc;
using bulkbuy.api.Models.DTOs;
using bulkbuy.api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace bulkbuy.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                var group = await _groupService.CreateGroupAsync(request, userId);
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                var userId = GetCurrentUserId();
                var groups = await _groupService.GetAllGroupsAsync(userId);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my-groups")]
        public async Task<IActionResult> GetMyGroups()
        {
            try
            {
                var userId = GetCurrentUserId();
                var groups = await _groupService.GetUserGroupsAsync(userId);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var group = await _groupService.GetGroupByIdAsync(id, userId);
                
                if (group == null)
                    return NotFound(new { Message = "Group not found" });

                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGroup(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _groupService.JoinGroupAsync(id, userId);
                
                if (!success)
                    return BadRequest(new { Message = "Unable to join group. You may already be a member or the group doesn't exist." });

                return Ok(new { Message = "Successfully joined the group" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("{id}/leave")]
        public async Task<IActionResult> LeaveGroup(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _groupService.LeaveGroupAsync(id, userId);
                
                if (!success)
                    return BadRequest(new { Message = "Unable to leave group. You may not be a member or you're the creator." });

                return Ok(new { Message = "Successfully left the group" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _groupService.DeleteGroupAsync(id, userId);
                
                if (!success)
                    return BadRequest(new { Message = "Unable to delete group. You may not be the creator or the group doesn't exist." });

                return Ok(new { Message = "Group successfully deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
