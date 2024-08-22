using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TaskManagement_API.Models
{
	public class Tasks
	{
		[BsonId]
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
		public string Priority { get; set; } = string.Empty;
		public string ListId { get; set; } = string.Empty;
		public string GroupId { get; set; } = string.Empty;
		public string[] AssignedUsers { get; set; } 
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
