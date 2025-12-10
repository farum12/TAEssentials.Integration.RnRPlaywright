using RestSharp;
using TAEssentials.BE.DTOs.Requests;

namespace TAEssentials.BE.Clients;

public class CouponsClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public CouponsClient()
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

    public async Task<RestResponse> ValidateCouponAsync(string code)
    {
        var request = new RestRequest($"/api/Coupons/validate/{code}", Method.Get);
        return await GetAsync(request);
    }

    public async Task<RestResponse> GetAllCouponsAsync()
    {
        var request = new RestRequest("/api/Coupons/admin/coupons", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> CreateCouponAsync(CreateCouponRequest createCouponRequest)
    {
        var request = new RestRequest("/api/Coupons/admin/coupons", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(createCouponRequest);
        return await PostAsync(request);
    }

    public async Task<RestResponse> UpdateCouponAsync(int couponId, UpdateCouponRequest updateCouponRequest)
    {
        var request = new RestRequest($"/api/Coupons/admin/coupons/{couponId}", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(updateCouponRequest);
        return await PutAsync(request);
    }

    public async Task<RestResponse> DeleteCouponAsync(int couponId)
    {
        var request = new RestRequest($"/api/Coupons/admin/coupons/{couponId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse> GetCouponUsageAsync(int couponId)
    {
        var request = new RestRequest($"/api/Coupons/admin/coupons/{couponId}/usage", Method.Get);
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
