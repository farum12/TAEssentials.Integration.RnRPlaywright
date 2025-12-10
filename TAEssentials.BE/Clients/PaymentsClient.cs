using RestSharp;
using TAEssentials.BE.DTOs.Requests;
using TAEssentials.BE.Enums;

namespace TAEssentials.BE.Clients;

public class PaymentsClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public PaymentsClient()
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

    public async Task<RestResponse> ProcessPaymentAsync(PaymentRequest paymentRequest)
    {
        var request = new RestRequest("/api/payments/process", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(paymentRequest);
        return await PostAsync(request);
    }

    public async Task<RestResponse> GetTransactionsAsync()
    {
        var request = new RestRequest("/api/payments/transactions", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> GetTransactionByIdAsync(int transactionId)
    {
        var request = new RestRequest($"/api/payments/transactions/{transactionId}", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> RefundPaymentAsync(RefundRequest refundRequest)
    {
        var request = new RestRequest("/api/payments/refund", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(refundRequest);
        return await PostAsync(request);
    }

    public async Task<RestResponse> GetAdminTransactionsAsync(PaymentStatus? status = null)
    {
        var request = new RestRequest("/api/payments/admin/transactions", Method.Get);
        AddAuthHeader(request);
        
        if (status.HasValue)
            request.AddQueryParameter("status", status.Value);

        return await GetAsync(request);
    }

    public async Task<RestResponse> GetPaymentStatisticsAsync()
    {
        var request = new RestRequest("/api/payments/admin/statistics", Method.Get);
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
