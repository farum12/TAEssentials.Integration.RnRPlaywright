namespace TAEssentials.BE.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public string? Isbn { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public string? StockStatus { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}
