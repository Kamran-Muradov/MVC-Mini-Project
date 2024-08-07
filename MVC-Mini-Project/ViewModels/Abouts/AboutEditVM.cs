﻿using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Abouts
{
    public class AboutEditVM
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
