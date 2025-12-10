using RestSharp;
using TAEssentials.BE.DTOs.Requests;

namespace TAEssentials.BE.Clients;

public class ProfileClient : BaseClient
{
    private const string BaseUrl = "http://localhost:5052";
    private string? _authToken;

    public ProfileClient()
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

    public async Task<RestResponse> GetProfileAsync()
    {
        var request = new RestRequest("/api/users/profile", Method.Get);
        AddAuthHeader(request);
        return await GetAsync(request);
    }

    public async Task<RestResponse> UpdateProfileAsync(UpdateProfileRequest updateProfileRequest)
    {
        var request = new RestRequest("/api/users/profile", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(updateProfileRequest);
        return await PutAsync(request);
    }

    public async Task<RestResponse> AddAddressAsync(AddAddressRequest addAddressRequest)
    {
        var request = new RestRequest("/api/users/profile/addresses", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(addAddressRequest);
        return await PostAsync(request);
    }

    public async Task<RestResponse> UpdateAddressAsync(int addressId, AddAddressRequest updateAddressRequest)
    {
        var request = new RestRequest($"/api/users/profile/addresses/{addressId}", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(updateAddressRequest);
        return await PutAsync(request);
    }

    public async Task<RestResponse> DeleteAddressAsync(int addressId)
    {
        var request = new RestRequest($"/api/users/profile/addresses/{addressId}", Method.Delete);
        AddAuthHeader(request);
        return await DeleteAsync(request);
    }

    public async Task<RestResponse> SetDefaultAddressAsync(int addressId)
    {
        var request = new RestRequest($"/api/users/profile/addresses/{addressId}/set-default", Method.Put);
        AddAuthHeader(request);
        return await PutAsync(request);
    }

    public async Task<RestResponse> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        var request = new RestRequest("/api/users/profile/change-password", Method.Put);
        AddAuthHeader(request);
        request.AddJsonBody(changePasswordRequest);
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
