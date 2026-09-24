using System.ComponentModel.DataAnnotations;

namespace MaisonFleurie.Models;

public class LoginModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}

public class SignUpModel
{
    [Required(ErrorMessage = "Please tell us your name")]
    public string FullName { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, MinLength(8, ErrorMessage = "At least 8 characters")]
    public string Password { get; set; } = "";

    [Required, Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; } = "";
}