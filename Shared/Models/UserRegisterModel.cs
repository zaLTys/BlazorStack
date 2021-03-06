using System.ComponentModel.DataAnnotations;

namespace BlazorStack.Shared.Models
{
    public class UserRegisterModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [StringLength(16, ErrorMessage = "Username too long (16max)")]
        public string Username { get; set; }
        public string Bio { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public int StartUnitId { get; set; } = 1;
        [Range(0, 1000, ErrorMessage = "Between 0 and 1k please")]
        public int Points { get; set; } = 100;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept TnC")]
        public bool AcceptedTermsAgreements { get; set; } = true;
    }
}
