namespace EcommVendor.Api.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool IsDeleted { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
}
