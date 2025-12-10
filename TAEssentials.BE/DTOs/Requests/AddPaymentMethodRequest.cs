using TAEssentials.BE.Enums;

namespace TAEssentials.BE.DTOs.Requests;

public class AddPaymentMethodRequest
{
    public PaymentMethodType Type { get; set; }
    public string? CardHolderName { get; set; }
    public string? CardNumber { get; set; }
    public string? ExpiryMonth { get; set; }
    public string? ExpiryYear { get; set; }
    public string? Cvv { get; set; }
    public string? PayPalEmail { get; set; }
}
