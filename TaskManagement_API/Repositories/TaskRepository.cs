using MongoDB.Driver;
using TaskManagement_API.Data;
using TaskManagement_API.Interfaces;
using TaskManagement_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagement_API.Repositories
{
	public class TaskRepository : ITaskRepository
	{
		private readonly MongoDbContext _context;

		public TaskRepository(MongoDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Tasks>> GetAllAsync()
		{
			return await _context.Tasks.Find(task => true).ToListAsync();
		}

		public async Task<Tasks> GetByIdAsync(Guid id)
		{
			return await _context.Tasks.Find(task => task.Id == id).FirstOrDefaultAsync();
		}

		public async Task CreateAsync(Tasks task)
		{
			await _context.Tasks.InsertOneAsync(task);
		}

		public async Task UpdateAsync(Guid id, Tasks taskIn)
		{
			await _context.Tasks.ReplaceOneAsync(task => task.Id == id, taskIn);
		}

		public async Task DeleteAsync(Guid id)
		{
			await _context.Tasks.DeleteOneAsync(task => task.Id == id);
		}
	}
}
