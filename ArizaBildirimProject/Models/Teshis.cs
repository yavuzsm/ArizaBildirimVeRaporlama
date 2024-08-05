﻿using System.ComponentModel.DataAnnotations;

namespace ArizaBildirimProject.Models
{
    public class Teshis
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public int RaporId { get; set; }
        public Rapor? Rapor { get; set; }
    }
}
