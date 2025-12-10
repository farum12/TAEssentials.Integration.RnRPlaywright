using RestSharp;
using TAEssentials.BE.DTOs.Requests;
using TAEssentials.BE.Models;

namespace TAEssentials.BE.Clients;

public class CartClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public CartClient()
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

    public async Task<RestResponse<Cart>> GetCartAsync()
    {
        var request = new RestRequest("/api/Cart", Method.Get);
        AddAuthHeader(request);
        return await GetAsync<Cart>(request);
    }

    public async Task<RestResponse<Cart>> AddToCartAsync(AddToCartRequest addToCartRequest)
    {
        var request = new RestRequest("/api/Cart/items", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(addToCartRequest);
        return await PostAsync<Cart>(request);
    }

    public async Task<RestResponse<Cart>> UpdateCartItemAsync(int itemId, UpdateCartItemRequest updateRequest)
    {
        var request = new RestRequest($"/api/Cart/items/{itemId}", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(updateRequest);
        return await PutAsync<Cart>(request);
    }

    public async Task<RestResponse<Cart>> RemoveCartItemAsync(int itemId)
    {
        var request = new RestRequest($"/api/Cart/items/{itemId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync<Cart>(request);
    }

    public async Task<RestResponse<Cart>> ClearCartAsync()
    {
        var request = new RestRequest("/api/Cart", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync<Cart>(request);
    }

    public async Task<RestResponse<Order>> CheckoutAsync()
    {
        var request = new RestRequest("/api/Cart/checkout", Method.Post);
        AddAuthHeader(request);
        return await PostAsync<Order>(request);
    }

    public async Task<RestResponse> ApplyCouponAsync(ApplyCouponRequest couponRequest)
    {
        var request = new RestRequest("/api/Cart/apply-coupon", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(couponRequest);
        return await PostAsync(request);
    }

    public async Task<RestResponse> RemoveCouponAsync()
    {
        var request = new RestRequest("/api/Cart/remove-coupon", Method.Delete);
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
