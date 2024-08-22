using MongoDB.Driver;
using TaskManagement_API.Data;
using TaskManagement_API.Models;

namespace TaskManagement_API.Repositories
{
	public class ListRepository : IListRepository
	{

		private readonly MongoDbContext _context;

		public ListRepository(MongoDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<List>> GetAllAsync()
		{
			return await _context.Lists.Find(_ => true).ToListAsync();
		}

		public async Task<List> GetByIdAsync(Guid id)
		{
			return await _context.Lists.Find(list => list.Id == id).FirstOrDefaultAsync();
		}

		public async Task CreateAsync(List list)
		{
			await _context.Lists.InsertOneAsync(list);
		}

		public async Task UpdateAsync(List list)
		{
			await _context.Lists.ReplaceOneAsync(l => l.Id == list.Id, list);
		}

		public async Task DeleteAsync(Guid id)
		{
			await _context.Lists.DeleteOneAsync(l => l.Id == id);
		}
	}
}
