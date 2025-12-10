namespace TAEssentials.BE.Models;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Author { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
}
