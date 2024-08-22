namespace TaskManagement_API.DTO
{
	public class GroupCreateDto
	{
		public string Name { get; set; } = string.Empty;
		public List<Guid> Lists { get; set; } = new List<Guid>();
	}
}
