namespace TaskManagement_API.DTO
{
	public class GroupUpdateDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<Guid> Lists { get; set; } = new List<Guid>();
	}
}
