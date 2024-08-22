using Microsoft.AspNetCore.Mvc;
using TaskManagement_API.DTO;
using TaskManagement_API.Models;
using TaskManagement_API.Services;

namespace TaskManagement_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupController : ControllerBase
	{
		private readonly GroupService _groupService;

		public GroupController(GroupService groupService)
		{
			_groupService = groupService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<List>>> GetLists()
		{
			var lists = await _groupService.GetAllGroupsAsync();
			return Ok(lists);
		}

		[HttpPost]
		public async Task<ActionResult<Group>> CreateGroup([FromBody] GroupCreateDto groupCreateDto)
		{
			var group = new Group
			{
				Name = groupCreateDto.Name,
				Lists = groupCreateDto.Lists
			};

			await _groupService.AddGroupAsync(group);

			return CreatedAtAction(nameof(GetGroupById), new { id = group.Id }, group);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] GroupUpdateDto groupUpdateDto)
		{
			if (id != groupUpdateDto.Id)
			{
				return BadRequest("Group ID mismatch");
			}

			var existingGroup = await _groupService.GetGroupByIdAsync(id);
			if (existingGroup == null)
			{
				return NotFound();
			}

			existingGroup.Name = groupUpdateDto.Name;
			existingGroup.Lists = groupUpdateDto.Lists;

			await _groupService.UpdateGroupAsync(existingGroup);

			return NoContent();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Group>> GetGroupById(Guid id)
		{
			var group = await _groupService.GetGroupByIdAsync(id);
			if (group == null)
			{
				return NotFound();
			}
			return Ok(group);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGroup(Guid id)
		{
			var existingGroup = await _groupService.GetGroupByIdAsync(id);
			if (existingGroup == null)
			{
				return NotFound();
			}

			await _groupService.DeleteGroupAsync(id);

			return NoContent();
		}
	}
}
