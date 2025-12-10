using TAEssentials.BE.Enums;

namespace TAEssentials.BE.DTOs.Requests;

public class UpdateCouponRequest
{
    public string? Code { get; set; }
    public double? Value { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int? MaxUsesTotal { get; set; }
    public bool? IsActive { get; set; }
}
