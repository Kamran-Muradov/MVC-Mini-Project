﻿namespace MVC_Mini_Project.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool SoftDeleted { get; set; } = false;
    }
}
