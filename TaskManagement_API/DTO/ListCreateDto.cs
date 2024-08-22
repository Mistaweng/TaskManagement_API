namespace TaskManagement_API.DTO
{
	public class ListCreateDto
	{
		public string Name { get; set; } = string.Empty;
		public List<Guid> Tasks { get; set; } = new List<Guid>();
		public string GroupId { get; set; } = string.Empty;
	}
}
