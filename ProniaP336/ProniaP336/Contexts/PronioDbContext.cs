using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProniaP336.Models;

namespace ProniaP336.Contexts;

public class PronioDbContext : IdentityDbContext<AppUser>
{
    public PronioDbContext(DbContextOptions<PronioDbContext> options) : base(options)
    {
    }

    public DbSet<Slider> Sliders { get; set; } = null!;
    public DbSet<Shipping> Shippings { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Setting> Settings { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<ProductTag> ProductTags { get; set; } = null!;
}