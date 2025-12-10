namespace TAEssentials.BE.DTOs.Requests;

public class PaymentRequest
{
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }
}
