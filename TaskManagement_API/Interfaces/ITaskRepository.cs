using TaskManagement_API.Models;

namespace TaskManagement_API.Interfaces
{
	public interface ITaskRepository
	{
		Task<IEnumerable<Tasks>> GetAllAsync();
		Task<Tasks> GetByIdAsync(Guid id);
		Task CreateAsync(Tasks task);
		Task UpdateAsync(Guid id, Tasks taskIn);
		Task DeleteAsync(Guid id);
	}
}