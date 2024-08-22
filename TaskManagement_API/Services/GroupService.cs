using TaskManagement_API.Models;
using TaskManagement_API.Repositories;

namespace TaskManagement_API.Services
{
	public class GroupService
	{
		private readonly IGroupRepository _groupRepository;

		public GroupService(IGroupRepository groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<IEnumerable<Group>> GetAllGroupsAsync()
		{
			return await _groupRepository.GetAllAsync();
		}

		public async Task AddGroupAsync(Group group)
		{
			await _groupRepository.CreateAsync(group);
		}

		public async Task<Group?> GetGroupByIdAsync(Guid id)
		{
			return await _groupRepository.GetByIdAsync(id);
		}

		public async Task UpdateGroupAsync(Group group)
		{
			await _groupRepository.UpdateAsync(group);
		}

		public async Task DeleteGroupAsync(Guid id)
		{
			await _groupRepository.DeleteAsync(id);
		}
	}
}
