namespace MVC_Mini_Project.Models
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
    }
}
