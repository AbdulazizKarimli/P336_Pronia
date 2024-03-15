using System.ComponentModel.DataAnnotations;

namespace ProniaP336.Areas.Admin.ViewModels.ProductViewModels;

public class ProductUpdateViewModel
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int DiscountPercent { get; set; }
    public int Rating { get; set; }
    public IFormFile? Image { get; set; }
    public int CategoryId { get; set; }
}