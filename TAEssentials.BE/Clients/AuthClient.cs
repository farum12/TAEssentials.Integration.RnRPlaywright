using RestSharp;
using TAEssentials.BE.DTOs.Requests;
using TAEssentials.BE.DTOs.Responses;
using TAEssentials.BE.Models;

namespace TAEssentials.BE.Clients;

public class AuthClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public AuthClient()
    {
        _restClient = new RestClient(BaseUrl);
    }

    protected override async Task Authorize()
    {
        // Authentication is handled per request via token
        await Task.CompletedTask;
    }

    public void SetAuthToken(string token)
    {
        _authToken = token;
    }

    public async Task<RestResponse<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var request = new RestRequest("/api/Users/register", Method.Post);
        request.AddJsonBody(registerRequest);
        return await PostAsync<RegisterResponse>(request);
    }

    public async Task<RestResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        var request = new RestRequest("/api/Users/login", Method.Post);
        request.AddJsonBody(loginRequest);
        var response = await PostAsync<LoginResponse>(request);
        
        if (response.IsSuccessful && response.Data?.Token != null)
        {
            _authToken = response.Data.Token;
        }
        
        return response;
    }

    public async Task<RestResponse> LogoutAsync()
    {
        var request = new RestRequest("/api/Users/logout", Method.Post);
        AddAuthHeader(request);
        return await PostAsync(request);
    }

    public async Task<RestResponse<User>> GetUserByIdAsync(int userId)
    {
        var request = new RestRequest($"/api/Users/{userId}", Method.Get);
        AddAuthHeader(request);
        return await GetAsync<User>(request);
    }

    public async Task<RestResponse> GetSessionAsync()
    {
        var request = new RestRequest("/api/Session", Method.Get);
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
