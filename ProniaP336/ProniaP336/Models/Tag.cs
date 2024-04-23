using System.ComponentModel.DataAnnotations;

namespace ProniaP336.Models;

public class Tag
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
    public ICollection<ProductTag> ProductTags { get; set; }
}