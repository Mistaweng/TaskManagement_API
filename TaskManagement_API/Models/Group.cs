using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TaskManagement_API.Models
{
	public class Group
	{
		[BsonId]
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<Guid> Lists { get; set; } = new List<Guid>();
	}
}
