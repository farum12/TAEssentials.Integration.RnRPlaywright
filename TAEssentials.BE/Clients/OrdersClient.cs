using RestSharp;
using TAEssentials.BE.DTOs.Requests;
using TAEssentials.BE.Models;

namespace TAEssentials.BE.Clients;

public class OrdersClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public OrdersClient()
    {
        _restClient = new RestClient(BaseUrl);
    }

    protected override async Task Authorize()
    {
        await Task.CompletedTask;
    }

    public void SetAuthToken(string token)
    {
        _authToken = token;
    }

    public async Task<RestResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest)
    {
        var request = new RestRequest("/api/Orders/create", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(createOrderRequest);
        return await PostAsync(request);
    }

    public async Task<RestResponse<Order>> PlaceOrderAsync(PlaceOrderRequest placeOrderRequest)
    {
        var request = new RestRequest("/api/Orders/place", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(placeOrderRequest);
        return await PostAsync<Order>(request);
    }

    public async Task<RestResponse<List<Order>>> GetAllOrdersAsync()
    {
        var request = new RestRequest("/api/Orders", Method.Get);
        AddAuthHeader(request);
        return await GetAsync<List<Order>>(request);
    }

    public async Task<RestResponse<List<Order>>> GetMyOrdersAsync()
    {
        var request = new RestRequest("/api/Orders/my-orders", Method.Get);
        AddAuthHeader(request);
        return await GetAsync<List<Order>>(request);
    }

    public async Task<RestResponse<Order>> GetOrderByIdAsync(int orderId)
    {
        var request = new RestRequest($"/api/Orders/{orderId}", Method.Get);
        AddAuthHeader(request);
        return await GetAsync<Order>(request);
    }

    public async Task<RestResponse> DeleteOrderAsync(int orderId)
    {
        var request = new RestRequest($"/api/Orders/{orderId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse<Order>> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusRequest statusRequest)
    {
        var request = new RestRequest($"/api/Orders/{orderId}/status", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(statusRequest);
        return await PutAsync<Order>(request);
    }

    public async Task<RestResponse> GetPendingOrdersAsync()
    {
        var request = new RestRequest("/api/Orders/pending", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> CancelOrderAsync(int orderId)
    {
        var request = new RestRequest($"/api/Orders/{orderId}/cancel", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    private void AddAuthHeader(RestRequest request)
    {
        if (!string.IsNullOrEmpty(_authToken))
        {
            request.AddHeader("Authorization", $"Bearer {_authToken}");
        }
    }
}
