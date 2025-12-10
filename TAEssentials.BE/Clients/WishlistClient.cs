using RestSharp;

namespace TAEssentials.BE.Clients;

public class WishlistClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public WishlistClient()
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

    public async Task<RestResponse> GetWishlistAsync()
    {
        var request = new RestRequest("/api/Wishlist", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> ClearWishlistAsync()
    {
        var request = new RestRequest("/api/Wishlist", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse> AddToWishlistAsync(int productId)
    {
        var request = new RestRequest($"/api/Wishlist/items/{productId}", Method.Post);
        AddAuthHeader(request);
        return await PostAsync(request);
    }

    public async Task<RestResponse> RemoveFromWishlistAsync(int productId)
    {
        var request = new RestRequest($"/api/Wishlist/items/{productId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse> CheckProductInWishlistAsync(int productId)
    {
        var request = new RestRequest($"/api/Wishlist/check/{productId}", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> MoveWishlistToCartAsync()
    {
        var request = new RestRequest("/api/Wishlist/move-to-cart", Method.Post);
        AddAuthHeader(request);
        return await PostAsync(request);
    }

    public async Task<RestResponse> GetWishlistCountAsync()
    {
        var request = new RestRequest("/api/Wishlist/count", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    private void AddAuthHeader(RestRequest request)
    {
        if (!string.IsNullOrEmpty(_authToken))
        {
            request.AddHeader("Authorization", $"Bearer {_authToken}");
        }
    }
}
