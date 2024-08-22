using Microsoft.AspNetCore.Mvc;
using TaskManagement_API.Models;
using TaskManagement_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TaskManagement_API.DTO;

namespace TaskManagement_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly TaskService _taskService;

		public TaskController(TaskService taskService)
		{
			_taskService = taskService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
		{
			var tasks = await _taskService.GetAllTasksAsync();
			return Ok(tasks);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Tasks>> GetTask(Guid id)
		{
			var task = await _taskService.GetTaskByIdAsync(id);
			if (task == null) return NotFound();
			return Ok(task);
		}

		[HttpPost]
		public async Task<ActionResult<Tasks>> CreateTask([FromBody] TaskCreateDto taskCreateDto)
		{
			var task = new Tasks
			{
				Title = taskCreateDto.Title,
				Description = taskCreateDto.Description,
				Status = taskCreateDto.Status,
				Priority = taskCreateDto.Priority,
				ListId = taskCreateDto.ListId,
				GroupId = taskCreateDto.GroupId,
				AssignedUsers = taskCreateDto.AssignedUsers,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await _taskService.AddTaskAsync(task);
			return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskUpdateDto taskUpdateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingTask = await _taskService.GetTaskByIdAsync(id);
			if (existingTask == null)
			{
				return NotFound();
			}

			existingTask.Title = taskUpdateDto.Title;
			existingTask.Description = taskUpdateDto.Description;
			existingTask.Status = taskUpdateDto.Status;
			existingTask.Priority = taskUpdateDto.Priority;
			existingTask.UpdatedAt = taskUpdateDto.UpdatedAt;

			await _taskService.UpdateTaskAsync(id, taskUpdateDto);

			return Ok(taskUpdateDto);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTask(Guid id)
		{
			var task = await _taskService.GetTaskByIdAsync(id);
			if (task == null) return NotFound();

			await _taskService.DeleteTaskAsync(id);
			return NoContent();
		}
	}
}


