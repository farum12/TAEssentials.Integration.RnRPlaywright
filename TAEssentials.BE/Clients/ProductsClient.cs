using RestSharp;
using TAEssentials.BE.DTOs.Requests;
using TAEssentials.BE.Models;

namespace TAEssentials.BE.Clients;

public class ProductsClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public ProductsClient()
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

    public async Task<RestResponse<List<Product>>> GetAllProductsAsync(
        string? searchTerm = null,
        string? genre = null,
        string? author = null,
        double? minPrice = null,
        double? maxPrice = null,
        string sortBy = "name",
        string sortOrder = "asc")
    {
        var request = new RestRequest("/api/Products", Method.Get);
        
        if (!string.IsNullOrEmpty(searchTerm))
            request.AddQueryParameter("searchTerm", searchTerm);
        if (!string.IsNullOrEmpty(genre))
            request.AddQueryParameter("genre", genre);
        if (!string.IsNullOrEmpty(author))
            request.AddQueryParameter("author", author);
        if (minPrice.HasValue)
            request.AddQueryParameter("minPrice", minPrice.Value);
        if (maxPrice.HasValue)
            request.AddQueryParameter("maxPrice", maxPrice.Value);
        
        request.AddQueryParameter("sortBy", sortBy);
        request.AddQueryParameter("sortOrder", sortOrder);

        return await GetAsync<List<Product>>(request);
    }

    public async Task<RestResponse<Product>> GetProductByIdAsync(int productId)
    {
        var request = new RestRequest($"/api/Products/{productId}", Method.Get);
        return await GetAsync<Product>(request);
    }

    public async Task<RestResponse<Product>> CreateProductAsync(Product product)
    {
        var request = new RestRequest("/api/Products", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(product);
        return await PostAsync<Product>(request);
    }

    public async Task<RestResponse> UpdateProductAsync(int productId, Product product)
    {
        var request = new RestRequest($"/api/Products/{productId}", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(product);
        return await PutAsync(request);
    }

    public async Task<RestResponse> DeleteProductAsync(int productId)
    {
        var request = new RestRequest($"/api/Products/{productId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse> CheckProductAvailabilityAsync(int productId, int quantity = 1)
    {
        var request = new RestRequest($"/api/Products/{productId}/availability", Method.Get);
        request.AddQueryParameter("quantity", quantity);
        return await GetAsync(request);
    }

    public async Task<RestResponse> UpdateStockAsync(int productId, StockUpdateRequest stockUpdate)
    {
        var request = new RestRequest($"/api/Products/{productId}/stock", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(stockUpdate);
        return await PutAsync(request);
    }

    public async Task<RestResponse> IncreaseStockAsync(int productId, StockChangeRequest stockChange)
    {
        var request = new RestRequest($"/api/Products/{productId}/stock/increase", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(stockChange);
        return await PostAsync(request);
    }

    public async Task<RestResponse> DecreaseStockAsync(int productId, StockChangeRequest stockChange)
    {
        var request = new RestRequest($"/api/Products/{productId}/stock/decrease", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(stockChange);
        return await PostAsync(request);
    }

    private void AddAuthHeader(RestRequest request)
    {
        if (!string.IsNullOrEmpty(_authToken))
        {
            request.AddHeader("Authorization", $"Bearer {_authToken}");
        }
    }
}
