using RestSharp;
using TAEssentials.BE.DTOs.Requests;

namespace TAEssentials.BE.Clients;

public class PaymentMethodsClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public PaymentMethodsClient()
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

    public async Task<RestResponse> GetPaymentMethodsAsync()
    {
        var request = new RestRequest("/api/payment-methods", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> AddPaymentMethodAsync(AddPaymentMethodRequest paymentMethodRequest)
    {
        var request = new RestRequest("/api/payment-methods", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(paymentMethodRequest);
        return await PostAsync(request);
    }

    public async Task<RestResponse> GetPaymentMethodByIdAsync(int paymentMethodId)
    {
        var request = new RestRequest($"/api/payment-methods/{paymentMethodId}", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> UpdatePaymentMethodAsync(int paymentMethodId, AddPaymentMethodRequest updateRequest)
    {
        var request = new RestRequest($"/api/payment-methods/{paymentMethodId}", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(updateRequest);
        return await PutAsync(request);
    }

    public async Task<RestResponse> DeletePaymentMethodAsync(int paymentMethodId)
    {
        var request = new RestRequest($"/api/payment-methods/{paymentMethodId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse> SetDefaultPaymentMethodAsync(int paymentMethodId)
    {
        var request = new RestRequest($"/api/payment-methods/{paymentMethodId}/set-default", Method.Put);
        AddAuthHeader(request);
        return await PutAsync(request);
    }

    private void AddAuthHeader(RestRequest request)
    {
        if (!string.IsNullOrEmpty(_authToken))
        {
            request.AddHeader("Authorization", $"Bearer {_authToken}");
        }
    }
}
