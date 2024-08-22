using TaskManagement_API.Models;

namespace TaskManagement_API.Repositories
{
	public interface IListRepository
	{
		Task<IEnumerable<List>> GetAllAsync();
		Task<List?> GetByIdAsync(Guid id);
		Task CreateAsync(List list);
		Task UpdateAsync(List list);
		Task DeleteAsync(Guid id);
	}
}