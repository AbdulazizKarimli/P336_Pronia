namespace ProniaP336.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    public int Rating { get; set; }
    public int DiscountPercent { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }   
    public DateTime UpdatedDate { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<ProductTag> ProductTags { get; set; }
}
