using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Username 100 karakterden uzun olamaz.")]
        public string Username { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "Sifre 100 karakterden uzun olamaz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifre ve şifre onayı eşleşmiyor.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
