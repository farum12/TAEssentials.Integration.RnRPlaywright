using TAEssentials.BE.Clients;
using TAEssentials.BE.DTOs.Requests;

namespace TAEssentials.UI.Utils;

public static class TokenProvider
{
    private static string? _adminToken;

    public static string? AdminToken => _adminToken;

    public static async Task RetrieveAndStoreAdminTokenAsync()
    {
        var authClient = new AuthClient();
        
        var loginRequest = new LoginRequest
        {
            //TODO: Move admin credentials to secure configuration
            Username = "admin",
            Password = "admin123"
        };

        var response = await authClient.LoginAsync(loginRequest);

        if (response.IsSuccessful && response.Data?.Token != null)
        {
            _adminToken = response.Data.Token;
        }
        else
        {
            throw new Exception($"Failed to retrieve admin token. Status: {response.StatusCode}, Error: {response.ErrorMessage}");
        }
    }

    public static void ClearToken()
    {
        _adminToken = null;
    }
}