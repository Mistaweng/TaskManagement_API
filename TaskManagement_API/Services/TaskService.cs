using Newtonsoft.Json;
using StackExchange.Redis;
using TaskManagement_API.DTO;
using TaskManagement_API.Interfaces;
using TaskManagement_API.Models;

namespace TaskManagement_API.Services
{
	public class TaskService
	{
		private readonly ITaskRepository _taskRepository;
		private readonly IDatabase _redisDb;

		public TaskService(ITaskRepository taskRepository, IConnectionMultiplexer redis)
		{
			_taskRepository = taskRepository;
			_redisDb = redis.GetDatabase();
		}

		public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
		{
			const string cacheKey = "tasks";
			var cachedTasks = await _redisDb.StringGetAsync(cacheKey);

			if (!string.IsNullOrEmpty(cachedTasks))
			{
				return JsonConvert.DeserializeObject<IEnumerable<Tasks>>(cachedTasks);
			}

			var tasks = await _taskRepository.GetAllAsync();
			await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(tasks), TimeSpan.FromMinutes(10));

			return tasks;
		}

		public async Task<Tasks> GetTaskByIdAsync(Guid id)
		{
			var cacheKey = $"task:{id}";
			var cachedTask = await _redisDb.StringGetAsync(cacheKey);

			if (!string.IsNullOrEmpty(cachedTask))
			{
				return JsonConvert.DeserializeObject<Tasks>(cachedTask);
			}

			var task = await _taskRepository.GetByIdAsync(id);
			if (task != null)
			{
				await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(task), TimeSpan.FromMinutes(10));
			}

			return task;
		}

		public async Task AddTaskAsync(Tasks task)
		{
			await _taskRepository.CreateAsync(task);
			await InvalidateCacheAsync();
		}

		public async Task UpdateTaskAsync(Guid id, TaskUpdateDto taskUpdateDto)
		{
			var existingTask = await _taskRepository.GetByIdAsync(id);
			if (existingTask == null)
			{
				throw new KeyNotFoundException("Task not found");
			}

			existingTask.Title = taskUpdateDto.Title;
			existingTask.Description = taskUpdateDto.Description;
			existingTask.Status = taskUpdateDto.Status;
			existingTask.Priority = taskUpdateDto.Priority;
			existingTask.UpdatedAt = DateTime.UtcNow;

			await _taskRepository.UpdateAsync(id, existingTask);

			await InvalidateCacheAsync(id.ToString());
		}

		public async Task DeleteTaskAsync(Guid id)
		{
			await _taskRepository.DeleteAsync(id);
			await InvalidateCacheAsync(id.ToString());
		}

		private async Task InvalidateCacheAsync(string id = null)
		{
			await _redisDb.KeyDeleteAsync("tasks");
			if (!string.IsNullOrEmpty(id))
			{
				await _redisDb.KeyDeleteAsync($"task:{id}");
			}
		}
	}
}












//using StackExchange.Redis;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TaskManagement_API.Interfaces;
//using TaskManagement_API.Models;
//using Newtonsoft.Json;
//using System;
//using TaskManagement_API.DTO;

//namespace TaskManagement_API.Services
//{
//	public class TaskService
//	{
//		private readonly ITaskRepository _taskRepository;
//		private readonly IDatabase _redisDb;

//		public TaskService(ITaskRepository taskRepository, IConnectionMultiplexer redis)
//		{
//			_taskRepository = taskRepository;
//			_redisDb = redis.GetDatabase();
//		}

//		public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
//		{
//			const string cacheKey = "tasks";
//			var cachedTasks = await _redisDb.StringGetAsync(cacheKey);

//			if (!string.IsNullOrEmpty(cachedTasks))
//			{
//				return JsonConvert.DeserializeObject<IEnumerable<Tasks>>(cachedTasks);
//			}

//			var tasks = await _taskRepository.GetAllAsync();
//			await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(tasks), TimeSpan.FromMinutes(10));

//			return tasks;
//		}

//		public async Task<Tasks> GetTaskByIdAsync(Guid id)
//		{
//			var cacheKey = $"task:{id}";
//			var cachedTask = await _redisDb.StringGetAsync(cacheKey);

//			if (!string.IsNullOrEmpty(cachedTask))
//			{
//				return JsonConvert.DeserializeObject<Tasks>(cachedTask);
//			}

//			var task = await _taskRepository.GetByIdAsync(id);
//			if (task != null)
//			{
//				await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(task), TimeSpan.FromMinutes(10));
//			}

//			return task;
//		}

//		public async Task AddTaskAsync(Tasks task)
//		{
//			await _taskRepository.CreateAsync(task);
//			await InvalidateCacheAsync();
//		}

//		public async Task UpdateTaskAsync(Guid id, TaskUpdateDto taskIn)
//		{
//			await _taskRepository.UpdateAsync(id, taskIn);
//			await InvalidateCacheAsync(id.ToString());
//		}

//		public async Task DeleteTaskAsync(Guid id)
//		{
//			await _taskRepository.DeleteAsync(id);
//			await InvalidateCacheAsync(id.ToString());
//		}

//		private async Task InvalidateCacheAsync(string id = null)
//		{
//			await _redisDb.KeyDeleteAsync("tasks");
//			if (!string.IsNullOrEmpty(id))
//			{
//				await _redisDb.KeyDeleteAsync($"task:{id}");
//			}
//		}
//	}
//}










////using AutoMapper;
////using Newtonsoft.Json;
////using StackExchange.Redis;
////using System.Collections.Generic;
////using System.Threading.Tasks;
////using TaskManagement_API.DTO;
////using TaskManagement_API.Interfaces;
////using TaskManagement_API.Models;

////namespace TaskManagement_API.Services
////{
////	public class TaskService
////	{
////		private readonly ITaskRepository _taskRepository;
////		private readonly IDatabase _redisDb;
////		private readonly IMapper _mapper;

////		public TaskService(ITaskRepository taskRepository, IConnectionMultiplexer redis, IMapper mapper)
////		{
////			_taskRepository = taskRepository;
////			_redisDb = redis.GetDatabase();
////			_mapper = mapper;
////		}

////		public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
////		{
////			const string cacheKey = "tasks";
////			var cachedTasks = await _redisDb.StringGetAsync(cacheKey);

////			if (!string.IsNullOrEmpty(cachedTasks))
////			{
////				var tasks = JsonConvert.DeserializeObject<IEnumerable<Tasks>>(cachedTasks);
////				return _mapper.Map<IEnumerable<TaskDto>>(tasks);
////			}

////			var tasksFromRepo = await _taskRepository.GetAllAsync();
////			await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(tasksFromRepo), TimeSpan.FromMinutes(10));

////			return _mapper.Map<IEnumerable<TaskDto>>(tasksFromRepo);
////		}

////		public async Task<TaskDto> GetTaskByIdAsync(Guid id)
////		{
////			var cacheKey = $"task:{id}";
////			var cachedTask = await _redisDb.StringGetAsync(cacheKey);

////			if (!string.IsNullOrEmpty(cachedTask))
////			{
////				var task = JsonConvert.DeserializeObject<Tasks>(cachedTask);
////				return _mapper.Map<TaskDto>(task);
////			}

////			var taskFromRepo = await _taskRepository.GetByIdAsync(id);
////			if (taskFromRepo != null)
////			{
////				await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(taskFromRepo), TimeSpan.FromMinutes(10));
////			}

////			return _mapper.Map<TaskDto>(taskFromRepo);
////		}

////		//public async Task AddTaskAsync(TaskCreateDto taskCreateDto)
////		//{
////		//	var task = _mapper.Map<Tasks>(taskCreateDto);
////		//	await _taskRepository.CreateAsync(task);
////		//	await InvalidateCacheAsync();
////		//}

////		public async Task AddTaskAsync(Tasks task)
////		{
////			await _taskRepository.CreateAsync(task);
////			await InvalidateCacheAsync();
////		}


////		public async Task UpdateTaskAsync(Guid id, TaskCreateDto taskIn)
////		{
////			var task = _mapper.Map<Tasks>(taskIn);
////			await _taskRepository.UpdateAsync(id, task);
////			await InvalidateCacheAsync(id.ToString());
////		}

////		public async Task DeleteTaskAsync(Guid id)
////		{
////			await _taskRepository.DeleteAsync(id);
////			await InvalidateCacheAsync(id.ToString());
////		}

////		private async Task InvalidateCacheAsync(string id = null)
////		{
////			await _redisDb.KeyDeleteAsync("tasks");
////			if (!string.IsNullOrEmpty(id))
////			{
////				await _redisDb.KeyDeleteAsync($"task:{id}");
////			}
////		}
////	}
////}











//////using Newtonsoft.Json;
//////using StackExchange.Redis;
//////using System.Collections.Generic;
//////using System.Threading.Tasks;
//////using TaskManagement_API.Interfaces;
//////using TaskManagement_API.Models;

//////namespace TaskManagement_API.Services
//////{
//////	public class TaskService
//////	{
//////		private readonly ITaskRepository _taskRepository;
//////		private readonly IDatabase _redisDb;

//////		public TaskService(ITaskRepository taskRepository, IConnectionMultiplexer redis)
//////		{
//////			_taskRepository = taskRepository;
//////			_redisDb = redis.GetDatabase();
//////		}

//////		public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
//////		{
//////			const string cacheKey = "tasks";
//////			var cachedTasks = await _redisDb.StringGetAsync(cacheKey);

//////			if (!string.IsNullOrEmpty(cachedTasks))
//////			{
//////				return JsonConvert.DeserializeObject<IEnumerable<Tasks>>(cachedTasks);
//////			}

//////			var tasks = await _taskRepository.GetAllAsync();
//////			await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(tasks), TimeSpan.FromMinutes(10));

//////			return tasks;
//////		}

//////		public async Task<Tasks> GetTaskByIdAsync(Guid id)
//////		{
//////			var cacheKey = $"task:{id}";
//////			var cachedTask = await _redisDb.StringGetAsync(cacheKey);

//////			if (!string.IsNullOrEmpty(cachedTask))
//////			{
//////				return JsonConvert.DeserializeObject<Tasks>(cachedTask);
//////			}

//////			var task = await _taskRepository.GetByIdAsync(id);
//////			if (task != null)
//////			{
//////				await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(task), TimeSpan.FromMinutes(10));
//////			}

//////			return task;
//////		}

//////		public async Task AddTaskAsync(Tasks task)
//////		{
//////			await _taskRepository.CreateAsync(task);
//////			await InvalidateCacheAsync();
//////		}

//////		public async Task UpdateTaskAsync(Guid id, Tasks taskIn)
//////		{
//////			await _taskRepository.UpdateAsync(id, taskIn);
//////			await InvalidateCacheAsync(id.ToString());
//////		}

//////		public async Task DeleteTaskAsync(Guid id)
//////		{
//////			await _taskRepository.DeleteAsync(id);
//////			await InvalidateCacheAsync(id.ToString());
//////		}

//////		private async Task InvalidateCacheAsync(string id = null)
//////		{
//////			await _redisDb.KeyDeleteAsync("tasks");
//////			if (!string.IsNullOrEmpty(id))
//////			{
//////				await _redisDb.KeyDeleteAsync($"task:{id}");
//////			}
//////		}
//////	}
//////}

