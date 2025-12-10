using TAEssentials.BE.Models;

namespace TAEssentials.BE.DTOs.Responses;

public class RegisterResponse
{
    public string? Message { get; set; }
    public UserInfo? User { get; set; }
}
