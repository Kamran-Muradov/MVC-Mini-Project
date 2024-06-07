﻿using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.Models
{
    public class Information : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int InformationIconId { get; set; }
        public InformationIcon InformationIcon { get; set; }
    }
}
