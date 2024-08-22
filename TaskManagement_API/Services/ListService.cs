using TaskManagement_API.Models;
using TaskManagement_API.Repositories;

namespace TaskManagement_API.Services
{
	public class ListService
	{
		private readonly IListRepository _listRepository;

		public ListService(IListRepository listRepository)
		{
			_listRepository = listRepository;
		}

		public async Task<IEnumerable<List>> GetAllListsAsync()
		{
			return await _listRepository.GetAllAsync();
		}

		public async Task AddListAsync(List list)
		{
			await _listRepository.CreateAsync(list);
		}

		public async Task<List?> GetListByIdAsync(Guid id)
		{
			return await _listRepository.GetByIdAsync(id);
		}

		public async Task UpdateListAsync(List list)
		{
			await _listRepository.UpdateAsync(list);
		}

		public async Task DeleteListAsync(Guid id)
		{
			await _listRepository.DeleteAsync(id);
		}
	}
}
