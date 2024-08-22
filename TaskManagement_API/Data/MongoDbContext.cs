using MongoDB.Driver;
using TaskManagement_API.Models;

namespace TaskManagement_API.Data
{
	public class MongoDbContext
	{
		private readonly IMongoDatabase _database;

		public MongoDbContext(IConfiguration configuration)
		{
			var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
			_database = client.GetDatabase("TaskManagementDb");
		}

		public IMongoCollection<Tasks> Tasks => _database.GetCollection<Tasks>("Tasks");
		public IMongoCollection<List> Lists => _database.GetCollection<List>("Lists");
		public IMongoCollection<Group> Groups => _database.GetCollection<Group>("Groups");
	}
}
