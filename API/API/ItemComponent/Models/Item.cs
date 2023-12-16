namespace API.ItemComponent.Models;

public class Item
{
    public Guid id { get; set; }
    public string title { get; set; } = String.Empty;
    public string imageURL { get; set; } = String.Empty;
    public string description { get; set; } = String.Empty;
    public int amountInStock { get; set; }
    public decimal discountPercentage { get; set; }
    public decimal price { get; set; }
    public Boolean isHidden { get; set; }
}