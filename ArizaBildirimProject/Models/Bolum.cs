using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class Bolum
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "İsim 100 karakterden uzun olamaz!.")]
        public string Name { get; set; } = "";

        public int DepartmentId { get; set; }
        public Departman? Departman { get; set; }

        public ICollection<Cihaz> Cihazs { get; set; } = new List<Cihaz>();
        public ICollection<Rapor> Raporlar { get; set; } = new List<Rapor>();

    }
}
