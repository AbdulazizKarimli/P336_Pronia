using System.ComponentModel.DataAnnotations;

namespace ProniaP336.Areas.Admin.ViewModels.ProductViewModels;

public class ProductCreateViewModel
{
    [Required(ErrorMessage = "Adivi yaz"), MaxLength(50)]
    public string Name { get; set; }
    [Required, MaxLength(500)]
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    [Range(0, 100)]
    public int DiscountPercent { get; set; }
    [Range(0,5)]
    public int Rating { get; set; }
    [Required]
    public IFormFile Image { get; set; }
    public int CategoryId { get; set; }
}