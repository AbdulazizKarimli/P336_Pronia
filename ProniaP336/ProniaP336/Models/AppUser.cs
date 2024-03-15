using Microsoft.AspNetCore.Identity;

namespace ProniaP336.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
    public bool IsActive { get; set; }
}