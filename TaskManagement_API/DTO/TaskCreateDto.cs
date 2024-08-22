namespace TaskManagement_API.DTO
{
	public class TaskCreateDto
	{
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
		public string Priority { get; set; } = string.Empty;
		public string ListId { get; set; } = string.Empty;
		public string GroupId { get; set; } = string.Empty;
		public string[] AssignedUsers { get; set; }
	}
}
