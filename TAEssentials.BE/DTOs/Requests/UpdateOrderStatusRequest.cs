using TAEssentials.BE.Enums;

namespace TAEssentials.BE.DTOs.Requests;

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
}
