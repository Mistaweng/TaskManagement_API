using TaskManagement_API.Models;

namespace TaskManagement_API.Repositories
{
	public interface IGroupRepository
	{
		Task<IEnumerable<Group>> GetAllAsync();
		Task CreateAsync(Group group);
		Task<Group?> GetByIdAsync(Guid id);
		Task UpdateAsync(Group group);
		Task DeleteAsync(Guid id);
	}
}
