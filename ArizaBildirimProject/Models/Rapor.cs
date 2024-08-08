using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArizaBildirimProject.Models
{
    public class Rapor
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int DepartmanId { get; set; }
        public Departman? Departman { get; set; }

        [Required]
        public int BolumId { get; set; }
       
        public Bolum? Bolum { get; set; }

        [Required]
        public int CihazId { get; set; }

        public Cihaz? Cihaz { get; set; }

        [Required]
        public int ArizaKisaTanimId { get; set; }

        public ArizaKisaTanim? ArizaKisaTanim { get; set; }

        [Required]
        public int ArizaTurId { get; set; }

        public ArizaTur? ArizaTur { get; set; }

        [Required]
        public int StatuId { get; set; }
        public Statu? Statu { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }

}
