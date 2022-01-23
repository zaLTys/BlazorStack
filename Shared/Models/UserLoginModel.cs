using System.ComponentModel.DataAnnotations;

namespace BlazorStack.Shared.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email Missing")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
