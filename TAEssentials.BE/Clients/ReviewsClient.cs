using RestSharp;
using TAEssentials.BE.DTOs.Requests;
using TAEssentials.BE.Models;

namespace TAEssentials.BE.Clients;

public class ReviewsClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public ReviewsClient()
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

    public async Task<RestResponse<Review>> CreateReviewAsync(int productId, CreateReviewRequest reviewRequest)
    {
        var request = new RestRequest($"/api/products/{productId}/Reviews", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(reviewRequest);
        return await PostAsync<Review>(request);
    }

    public async Task<RestResponse> GetProductReviewsAsync(
        int productId,
        int? rating = null,
        bool? verifiedOnly = null,
        string sortBy = "date",
        string sortOrder = "desc")
    {
        var request = new RestRequest($"/api/products/{productId}/Reviews", Method.Get);
        
        if (rating.HasValue)
            request.AddQueryParameter("rating", rating.Value);
        if (verifiedOnly.HasValue)
            request.AddQueryParameter("verifiedOnly", verifiedOnly.Value);
        
        request.AddQueryParameter("sortBy", sortBy);
        request.AddQueryParameter("sortOrder", sortOrder);

        return await GetAsync(request);
    }

    public async Task<RestResponse<Review>> GetReviewByIdAsync(int productId, int reviewId)
    {
        var request = new RestRequest($"/api/products/{productId}/Reviews/{reviewId}", Method.Get);
        return await GetAsync<Review>(request);
    }

    public async Task<RestResponse> DeleteReviewAsync(int productId, int reviewId)
    {
        var request = new RestRequest($"/api/products/{productId}/Reviews/{reviewId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse<Review>> GetMyReviewForProductAsync(int productId)
    {
        var request = new RestRequest($"/api/products/{productId}/my-review", Method.Get);
        AddAuthHeader(request);
        return await GetAsync<Review>(request);
    }

    public async Task<RestResponse> MarkReviewHelpfulAsync(int reviewId)
    {
        var request = new RestRequest($"/api/reviews/{reviewId}/helpful", Method.Post);
        AddAuthHeader(request);
        return await PostAsync(request);
    }

    public async Task<RestResponse<Review>> ModerateReviewAsync(int productId, int reviewId, ModerateReviewRequest moderateRequest)
    {
        var request = new RestRequest($"/api/products/{productId}/Reviews/{reviewId}/moderate", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(moderateRequest);
        return await PutAsync<Review>(request);
    }

    public async Task<RestResponse<List<Review>>> GetAllReviewsAdminAsync(bool includeHidden = true, int? productId = null)
    {
        var request = new RestRequest("/api/admin/reviews", Method.Get);
        AddAuthHeader(request);
        request.AddQueryParameter("includeHidden", includeHidden);
        
        if (productId.HasValue)
            request.AddQueryParameter("productId", productId.Value);

        return await GetAsync<List<Review>>(request);
    }

    private void AddAuthHeader(RestRequest request)
    {
        if (!string.IsNullOrEmpty(_authToken))
        {
            request.AddHeader("Authorization", $"Bearer {_authToken}");
        }
    }
}
