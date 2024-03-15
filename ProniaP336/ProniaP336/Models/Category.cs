namespace ProniaP336.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<Product>? Products { get; set; }
}