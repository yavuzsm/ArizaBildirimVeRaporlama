using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class ArizaTur
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public ICollection<Rapor> Raporlar { get; set; } = new List<Rapor>();

        public ICollection<ArizaKisaTanim> ArizaKisaTanimlar { get; set; } = new List<ArizaKisaTanim>();
    }
}
