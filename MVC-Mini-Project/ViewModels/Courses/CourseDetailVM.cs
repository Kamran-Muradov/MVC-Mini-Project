namespace MVC_Mini_Project.ViewModels.Courses
{
    public class CourseDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Instructor { get; set; }
        public int Rating { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StudentCount { get; set; }
        public List<CourseImageVM> Images { get; set; }
    }
}
