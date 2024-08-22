//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
//using System.Net.Http.Json;
//using TaskManagement_API.DTO;
//using TaskManagement_API.Models;

//namespace TaskManagement_API.Test.IntegrationTests
//{
//	public class GroupControllerTests : IClassFixture<WebApplicationFactory<Program>>
//	{
//		private readonly HttpClient _client;

//		public GroupControllerTests(WebApplicationFactory<Program> factory)
//		{
//			_client = factory.CreateClient();
//		}

//		[Fact]
//		public async Task GetGroups_ReturnsOkResponse()
//		{
//			var response = await _client.GetAsync("/api/group");

//			response.EnsureSuccessStatusCode();
//			var groups = await response.Content.ReadFromJsonAsync<IEnumerable<Group>>();
//			Assert.NotNull(groups);
//		}

//		[Fact]
//		public async Task CreateGroup_ReturnsCreatedResponse()
//		{
//			var groupCreateDto = new GroupCreateDto
//			{
//				Name = "Integration Test Group",
//				Lists = new List<Guid>()
//			};

//			var response = await _client.PostAsJsonAsync("/api/group", groupCreateDto);

//			response.EnsureSuccessStatusCode();
//			var createdGroup = await response.Content.ReadFromJsonAsync<Group>();
//			Assert.NotNull(createdGroup);
//			Assert.Equal("Integration Test Group", createdGroup.Name);
//		}

//		[Fact]
//		public async Task UpdateGroup_ReturnsNoContentResponse()
//		{
//			var groupId = Guid.NewGuid();
//			var groupUpdateDto = new GroupUpdateDto
//			{
//				Id = groupId,
//				Name = "Updated Integration Test Group",
//				Lists = new List<Guid>()
//			};

//			var response = await _client.PutAsJsonAsync($"/api/group/{groupId}", groupUpdateDto);

//			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
//		}

//		[Fact]
//		public async Task DeleteGroup_ReturnsNoContentResponse()
//		{
//			var groupId = Guid.NewGuid();

//			var response = await _client.DeleteAsync($"/api/group/{groupId}");

//			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
//		}
//	}
//}
