namespace TAEssentials.BE.DTOs.Requests;

public class PlaceOrderRequest
{
    public int UserId { get; set; }
    public List<OrderItemRequest>? Items { get; set; }
}
