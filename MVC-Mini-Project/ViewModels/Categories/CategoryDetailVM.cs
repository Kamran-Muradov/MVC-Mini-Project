namespace MVC_Mini_Project.ViewModels.Categories
{
    public class CategoryDetailVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public ICollection<string> Courses { get; set; }
        public int CourseCount { get; set; }
    }
}
