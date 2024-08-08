using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class Statu
    {   public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";


    }
}
