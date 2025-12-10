using TAEssentials.BE.Enums;

namespace TAEssentials.BE.DTOs.Requests;

public class CreateCouponRequest
{
    public string? Code { get; set; }
    public DiscountType Type { get; set; }
    public double Value { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int? MaxUsesTotal { get; set; }
}
