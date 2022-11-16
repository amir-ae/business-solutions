using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Shouldly;

namespace BusinessSolutions.Services.Ordering.API.Tests
{
    public class ProviderControllerTests : IClassFixture<InMemoryWebApplicationFactory<Program>>
    {
        private readonly InMemoryWebApplicationFactory<Program> _factory;

        public ProviderControllerTests(InMemoryWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/providers")]
        public async Task get_should_return_success(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/api/providers/?pageSize=1&pageIndex=0", 1, 0)]
        public async Task get_should_return_paginated_data(string url, int pageSize, int pageIndex)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<PaginatedResponseModel<ProviderResponse>>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.PageIndex.ShouldBe(pageIndex);
            responseEntity.PageSize.ShouldBe(pageSize);
            responseEntity.Data.Count().ShouldBe(pageSize);
        }

        [Theory]
        [LoadData("provider.ProviderId")]
        public async Task get_by_id_should_return_provider(int id)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/providers/{id}");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Provider>(responseContent);
            responseEntity.ShouldNotBeNull();
        }

        [Theory]
        [LoadData("provider")]
        public async Task get_by_id_should_return_right_provider(Provider request)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/providers/{request.ProviderId}");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<ProviderResponse>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.ProviderName.ShouldBe(request.ProviderName);
            responseEntity.ProviderId.ShouldBe(request.ProviderId);
        }

        [Theory]
        [LoadData("provider")]
        public async Task add_should_create_new_provider(AddProviderRequest request)
        {
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync($"/api/providers/", httpContent);

            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            response.Headers.Location?.ToString()?.ShouldContain("/api/providers/");
        }

        [Theory]
        [LoadData("provider.ProviderId")]
        public async Task delete_should_remove_existing_provider(int id)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/providers/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
