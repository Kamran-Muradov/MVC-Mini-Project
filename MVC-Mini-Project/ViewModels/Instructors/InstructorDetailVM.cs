namespace MVC_Mini_Project.ViewModels.Instructors
{
    public class InstructorDetailVM
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Field { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<InstructorSocialVM> InstructorSocials { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
