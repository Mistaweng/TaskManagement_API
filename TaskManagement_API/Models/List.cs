using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TaskManagement_API.Models
{
	public class List
	{
		[BsonId]
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<Guid> Tasks { get; set; } = new List<Guid>();
		public string GroupId { get; set; } = string.Empty;
		public string[] TaskIds { get; set; } = { string.Empty };
	}
}
