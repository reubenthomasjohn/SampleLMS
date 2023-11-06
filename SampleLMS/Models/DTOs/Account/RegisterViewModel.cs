using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DTOs.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password has to be 6 characters at least.")]
        public string Password { get; set; }
    }
}
