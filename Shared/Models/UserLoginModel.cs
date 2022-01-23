using System.ComponentModel.DataAnnotations;

namespace BlazorStack.Shared.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "PlaceholderErrorMessage")]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
