using MongoDB.Driver;
using TaskManagement_API.Data;
using TaskManagement_API.Models;

namespace TaskManagement_API.Repositories
{
	public class GroupRepository : IGroupRepository
	{
		private readonly MongoDbContext _context;

		public GroupRepository(MongoDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Group>> GetAllAsync()
		{
			return await _context.Groups.Find(_ => true).ToListAsync();
		}

		public async Task CreateAsync(Group group)
		{
			await _context.Groups.InsertOneAsync(group);
		}

		public async Task<Group?> GetByIdAsync(Guid id)
		{
			return await _context.Groups.Find(g => g.Id == id).FirstOrDefaultAsync();
		}

		public async Task UpdateAsync(Group group)
		{
			await _context.Groups.ReplaceOneAsync(g => g.Id == group.Id, group);
		}

		public async Task DeleteAsync(Guid id)
		{
			await _context.Groups.DeleteOneAsync(g => g.Id == id);
		}
	}
}
