﻿namespace MVC_Mini_Project.ViewModels.Students
{
    public class StudentDetailVM
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<string> Courses { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
