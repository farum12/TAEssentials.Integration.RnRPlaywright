using TAEssentials.BE.Enums;

namespace TAEssentials.BE.DTOs.Requests;

public class AddAddressRequest
{
    public AddressType AddressType { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public bool IsDefault { get; set; }
}
