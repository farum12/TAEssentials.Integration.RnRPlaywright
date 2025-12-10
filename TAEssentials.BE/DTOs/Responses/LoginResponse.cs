using TAEssentials.BE.Models;

namespace TAEssentials.BE.DTOs.Responses;

public class LoginResponse
{
    public string? Message { get; set; }
    public string? Token { get; set; }
    public UserInfo? User { get; set; }
}
