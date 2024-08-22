using Microsoft.AspNetCore.Mvc;
using TaskManagement_API.DTO;
using TaskManagement_API.Models;
using TaskManagement_API.Services;

namespace TaskManagement_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ListController : ControllerBase
	{
		private readonly ListService _listService;

		public ListController(ListService listService)
		{
			_listService = listService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<List>>> GetLists()
		{
			var lists = await _listService.GetAllListsAsync();
			return Ok(lists);
		}


		[HttpPost]
		public async Task<ActionResult<List>> CreateList([FromBody] ListCreateDto listCreateDto)
		{
			var list = new List
			{
				Name = listCreateDto.Name,
				Tasks = listCreateDto.Tasks,
				GroupId = listCreateDto.GroupId
			};

			await _listService.AddListAsync(list);

			return CreatedAtAction(nameof(GetListById), new { id = list.Id }, list);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateList(Guid id, [FromBody] ListUpdateDto listUpdateDto)
		{
			if (id != listUpdateDto.Id)
			{
				return BadRequest("List ID mismatch");
			}

			var existingList = await _listService.GetListByIdAsync(id);
			if (existingList == null)
			{
				return NotFound();
			}

			existingList.Name = listUpdateDto.Name;
			existingList.Tasks = listUpdateDto.Tasks;
			existingList.GroupId = listUpdateDto.GroupId;

			await _listService.UpdateListAsync(existingList);

			return NoContent();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<List>> GetListById(Guid id)
		{
			var list = await _listService.GetListByIdAsync(id);
			if (list == null)
			{
				return NotFound();
			}
			return Ok(list);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteList(Guid id)
		{
			var existingList = await _listService.GetListByIdAsync(id);
			if (existingList == null)
			{
				return NotFound();
			}

			await _listService.DeleteListAsync(id);
			return NoContent();
		}

	}
}