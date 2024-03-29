using System.ComponentModel.DataAnnotations;

namespace ProniaP336.ViewModels;

public class ForgotPasswordViewModel
{
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}