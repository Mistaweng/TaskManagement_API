//using Microsoft.AspNetCore.Mvc.Testing;
//using System.Net.Http.Json;
//using TaskManagement_API.DTO;
//using TaskManagement_API.Models;

//namespace TaskManagement_API.Test.IntegrationTests
//{
//	public class ListControllerTests : IClassFixture<WebApplicationFactory<program>>
//	{
//		private readonly HttpClient _client;

//		public ListControllerTests(WebApplicationFactory<> factory)
//		{
//			_client = factory.CreateClient();
//		}

//		[Fact]
//		public async Task GetLists_ReturnsOkResponse()
//		{
//			var response = await _client.GetAsync("/api/list");

//			response.EnsureSuccessStatusCode();
//			var lists = await response.Content.ReadFromJsonAsync<IEnumerable<List>>();
//			Assert.NotNull(lists);
//		}

//		[Fact]
//		public async Task CreateList_ReturnsCreatedResponse()
//		{
//			var listCreateDto = new ListCreateDto
//			{
//				Name = "Integration Test List",
//				GroupId = Guid.NewGuid().ToString(),
//				Tasks = new List<Guid>()
//			};

//			var response = await _client.PostAsJsonAsync("/api/list", listCreateDto);

//			response.EnsureSuccessStatusCode();
//			var createdList = await response.Content.ReadFromJsonAsync<List>();
//			Assert.NotNull(createdList);
//			Assert.Equal("Integration Test List", createdList.Name);
//		}

//		[Fact]
//		public async Task UpdateList_ReturnsNoContentResponse()
//		{
//			var listId = Guid.NewGuid();
//			var listUpdateDto = new ListUpdateDto
//			{
//				Id = listId,
//				Name = "Updated Integration Test List",
//				GroupId = Guid.NewGuid().ToString(),
//				Tasks = new List<Guid>()
//			};

//			var response = await _client.PutAsJsonAsync($"/api/list/{listId}", listUpdateDto);

//			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
//		}

//		[Fact]
//		public async Task DeleteList_ReturnsNoContentResponse()
//		{
//			var listId = Guid.NewGuid();

//			var response = await _client.DeleteAsync($"/api/list/{listId}");

//			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
//		}
//	}
//}
