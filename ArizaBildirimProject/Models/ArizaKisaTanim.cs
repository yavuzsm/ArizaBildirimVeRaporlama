using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class ArizaKisaTanim
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public int ArizaTurId { get; set; }
        public ArizaTur ArizaTur { get; set; }

        public ICollection<Rapor> Raporlar { get; set; } = new List<Rapor>();
    }
}
