using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class Cihaz
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "İsim 100 karakterden uzun olamaz.")]
        public string Name { get; set; } = "";

        public int BolumId { get; set; }
        public Bolum? Bolum { get; set; }

        public ICollection<Rapor> Raporlar { get; set; } = new List<Rapor>();

    }
}
