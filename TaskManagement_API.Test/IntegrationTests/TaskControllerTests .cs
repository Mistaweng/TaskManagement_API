//using System.Net.Http.Json;
//using Microsoft.AspNetCore.Mvc.Testing;
//using TaskManagement_API.DTO;
//using TaskManagement_API.Models;

//namespace TaskManagement_API.Test.IntegrationTests
//{
//	public class TaskControllerTests : IClassFixture<WebApplicationFactory<Program>>
//	{
//		private readonly HttpClient _client;

//		public TaskControllerTests(WebApplicationFactory<Program> factory)
//		{
//			_client = factory.CreateClient();
//		}

//		[Fact]
//		public async Task GetTasks_ReturnsOkResponse()
//		{
//			var response = await _client.GetAsync("/api/task");

//			response.EnsureSuccessStatusCode();
//			var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<Tasks>>();
//			Assert.NotNull(tasks);
//		}

//		[Fact]
//		public async Task CreateTask_ReturnsCreatedResponse()
//		{
//			var taskCreateDto = new TaskCreateDto
//			{
//				Title = "Integration Test Task",
//				Description = "Integration Test Task Description",
//				Status = "Pending",
//				Priority = "High",
//				ListId = Guid.NewGuid().ToString(),
//				GroupId = Guid.NewGuid().ToString(),
//				AssignedUsers = new List<string> { "User1", "User2" }.ToArray()
//			};

//			var response = await _client.PostAsJsonAsync("/api/task", taskCreateDto);

//			response.EnsureSuccessStatusCode();
//			var createdTask = await response.Content.ReadFromJsonAsync<Tasks>();
//			Assert.NotNull(createdTask);
//			Assert.Equal("Integration Test Task", createdTask.Title);
//		}

//		[Fact]
//		public async Task UpdateTask_ReturnsNoContentResponse()
//		{
//			var taskId = Guid.NewGuid();
//			var taskUpdateDto = new TaskUpdateDto
//			{
//				Title = "Updated Integration Test Task",
//				Description = "Updated Integration Test Task Description",
//				Status = "Completed",
//				Priority = "Medium",
//				UpdatedAt = DateTime.UtcNow
//			};

//			var response = await _client.PutAsJsonAsync($"/api/task/{taskId}", taskUpdateDto);

//			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
//		}

//		[Fact]
//		public async Task DeleteTask_ReturnsNoContentResponse()
//		{
//			var taskId = Guid.NewGuid();

//			var response = await _client.DeleteAsync($"/api/task/{taskId}");

//			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
//		}
//	}
//}
