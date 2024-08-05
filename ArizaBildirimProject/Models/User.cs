using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Username 100 karakterden uzun olamaz.")]
        public string Username { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "Sifre 100 karakterden uzun olamaz.")]
        public string PasswordHash { get; set; } = "";

        public int? DepartmanId { get; set; }
        public Departman? Departman { get; set; }

        public int? RoleId { get; set; } 
        public Role? Role { get; set; }


    }
}
