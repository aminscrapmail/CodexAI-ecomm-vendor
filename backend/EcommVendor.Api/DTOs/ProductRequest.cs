namespace EcommVendor.Api.DTOs;

public class ProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public string ModifiedBy { get; set; } = "vendor-user";
}
