namespace TAEssentials.BE.DTOs.Requests;

public class RefundRequest
{
    public string? TransactionId { get; set; }
    public double Amount { get; set; }
    public string? Reason { get; set; }
}
