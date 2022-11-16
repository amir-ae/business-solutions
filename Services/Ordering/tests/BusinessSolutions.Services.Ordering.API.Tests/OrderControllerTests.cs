using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace BusinessSolutions.Services.Ordering.API.Tests
{
    public class OrderControllerTests : IClassFixture<InMemoryWebApplicationFactory<Program>>
    {
        private readonly InMemoryWebApplicationFactory<Program> _factory;

        public OrderControllerTests(InMemoryWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/orders")]
        public async Task get_should_return_success(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/api/orders/?pageSize=1&pageIndex=0", 1, 0)]
        [InlineData("/api/orders/?pageSize=2&pageIndex=0", 2, 0)]
        [InlineData("/api/orders/?pageSize=1&pageIndex=1", 1, 1)]
        public async Task get_should_return_paginated_data(string url, int pageSize, int pageIndex)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<PaginatedResponseModel<OrderResponse>>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.PageIndex.ShouldBe(pageIndex);
            responseEntity.PageSize.ShouldBe(pageSize);
            responseEntity.Data.Count().ShouldBe(pageSize);
        }

        [Theory]
        [LoadData("order.Number")]
        public async Task get_by_number_should_return_order(string number)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/orders/{number}");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Order>(responseContent);
            responseEntity.ShouldNotBeNull();
        }

        [Theory]
        [LoadData("order")]
        public async Task get_by_id_should_return_right_order(Order request)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/orders/{request.Id}");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<OrderResponse>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Id.ShouldBe(request.Id);
            responseEntity.Number.ShouldBe(request.Number);
        }

        [Theory]
        [LoadData("order")]
        public async Task get_by_filter_should_return_right_orders(Order request)
        {
            var providerId = request.ProviderId.ToString();
            var providerName = request.Provider!.ProviderName!;
            var orderNumber = request.Number!.Substring(0, 3);
            var startTime = DateTimeOffset.Parse(request.Date.ToString()).AddDays(-1).ToString();
            var endTime = DateTimeOffset.Parse(request.Date.ToString()).AddDays(1).ToString();
            var itemName = request.Items!.First().Name!;
            var itemUnit = request.Items!.First().Unit!;

            var dict = new Dictionary<string, string>()
            {
                { nameof(providerId), providerId },
                { nameof(providerName), providerName },
                { nameof(orderNumber), orderNumber },
                { nameof(startTime), startTime },
                { nameof(endTime), endTime },
                { nameof(itemName), itemName },
                { nameof(itemUnit), itemUnit }
            };
            foreach (var key in dict.Keys.ToArray())
            {
                if (dict[key] == null) dict.Remove(key);
            }
         
            var uri = BuildUri($"/api/orders/filter", dict);

            var client = _factory.CreateClient();
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<PaginatedResponseModel<OrderResponse>>(responseContent);

            var responseData = responseEntity!.Data;

            responseData.All(i => i.ProviderId == int.Parse(providerId)).ShouldBeTrue();
            responseData.All(i => i.Provider!.ProviderName == providerName).ShouldBeTrue();
            responseData.All(i => DateTimeOffset.Compare(DateTimeOffset.Parse(startTime), i.Date) < 0
                    && DateTimeOffset.Compare(DateTimeOffset.Parse(endTime), i.Date) > 0).ShouldBeTrue();
            responseData.All(i => i.Items!.Any(x => x.Name == itemName)).ShouldBeTrue();
            responseData.All(i => i.Items!.Any(x => x.Unit == itemUnit)).ShouldBeTrue();
        }

        [Theory]
        [LoadData("order")]
        public async Task get_by_parital_filter_should_return_right_orders(Order request)
        {
            var providerId = request.ProviderId.ToString();
            var providerName = request.Provider!.ProviderName!;
            var orderNumber = request.Number!.Substring(0, 3);
            var startTime = DateTimeOffset.Parse(request.Date.ToString()).AddDays(-1).ToString();
            var endTime = DateTimeOffset.Parse(request.Date.ToString()).AddDays(1).ToString();
            var itemName = request.Items!.First().Name!;
            var itemUnit = request.Items!.First().Unit!;

            var dict = new Dictionary<string, string>()
            {
                { nameof(providerId), null! },
                { nameof(providerName), providerName },
                { nameof(orderNumber), null! },
                { nameof(startTime), startTime },
                { nameof(endTime), null! },
                { nameof(itemName), null! },
                { nameof(itemUnit), itemUnit }
            };
            foreach (var key in dict.Keys.ToArray())
            {
                if (dict[key] == null) dict.Remove(key);
            }

            var uri = BuildUri($"/api/orders/filter", dict);

            var client = _factory.CreateClient();
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<PaginatedResponseModel<OrderResponse>>(responseContent);

            var responseData = responseEntity!.Data;

            responseData.All(i => i.Provider!.ProviderName == providerName).ShouldBeTrue();
            responseData.All(i => DateTimeOffset.Compare(DateTimeOffset.Parse(startTime), i.Date) < 0)
                    .ShouldBeTrue();
            responseData.All(i => i.Items!.Any(x => x.Unit == itemUnit)).ShouldBeTrue();
        }

        [Theory]
        [LoadData("order")]
        public async Task add_should_create_new_order(AddOrderRequest request)
        {
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync($"/api/orders/", httpContent);

            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            response.Headers.Location?.ToString()?.ShouldContain("/api/orders/");
        }


        [Theory]
        [LoadData("order")]
        public async Task update_should_modify_existing_order(EditOrderRequest request)
        {
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/orders/{request.Number}", httpContent);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Order>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Number.ShouldBe(request.Number);
            responseEntity.ProviderId.ShouldBe(request.ProviderId);
        }

        [Theory]
        [LoadData("order.Number")]
        public async Task delete_should_returns_no_content_when_called_with_right_id(string number)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/orders/{number}");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        Uri BuildUri(string format, Dictionary<string, string>? dict = default)
        {
            var uri = new UriBuilder()
            {
                Path = format
            }.Uri;

            if (dict?.Count > 0)
            {
                foreach (var kvp in dict)
                {
                    uri = AddQuery(uri, kvp.Key, kvp.Value);
                }
            }
            return uri;
        }

        Uri AddQuery(Uri uri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value.ToString());

            var ub = new UriBuilder(uri);
            ub.Query = httpValueCollection.ToString();

            return ub.Uri;
        }
    }
}
