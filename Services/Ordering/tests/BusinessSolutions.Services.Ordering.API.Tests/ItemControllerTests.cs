using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BusinessSolutions.Services.Ordering.API.Tests
{
    public class ItemControllerTests : IClassFixture<InMemoryWebApplicationFactory<Program>>
    {
        private readonly InMemoryWebApplicationFactory<Program> _factory;

        public ItemControllerTests(InMemoryWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [LoadData("item.ItemId")]
        public async Task get_by_id_should_return_item_data(int id)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/items/{id}");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Item>(responseContent);
            responseEntity.ShouldNotBeNull();
        }

        [Theory]
        [LoadData("item")]
        public async Task get_by_id_should_return_right_data(Item request)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/items/{request.ItemId}");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<ItemResponse>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Name.ShouldBe(request.Name);
            responseEntity.Quantity.ShouldBe(request.Quantity);
            responseEntity.Unit.ShouldBe(request.Unit);
        }

        [Theory]
        [LoadData("item")]
        public async Task add_should_create_new_record(AddItemRequest request)
        {
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync($"/api/items/", httpContent);

            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            response.Headers.Location?.ToString()?.ShouldContain("/api/items/");
        }

        [Theory]
        [LoadData("item")]
        public async Task update_should_modify_existing_item(EditItemRequest request)
        {
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/items/{request.ItemId}", httpContent);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Item>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Name.ShouldBe(request.Name);
            responseEntity.Quantity.ShouldBe(request.Quantity);
            responseEntity.Unit.ShouldBe(request.Unit);
        }

        [Theory]
        [LoadData("item.ItemId")]
        public async Task delete_should_remove_existing_item(int id)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/items/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Theory]
        [LoadData("item.ItemId")]
        public async Task delete_should_returns_no_content_when_called_with_right_id(int id)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/items/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
