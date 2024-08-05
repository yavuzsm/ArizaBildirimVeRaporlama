using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class Departman
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "İsim 100 karakterden uzun olamaz.")]
        public string Name { get; set; } = "";

     

        public ICollection<Bolum> Bolumler { get; set; } = new List<Bolum>();

        public ICollection<Rapor> Raporlar { get; set; } = new List<Rapor>();

        public ICollection<User> Users { get; set; } = new List<User>();


    }
}
