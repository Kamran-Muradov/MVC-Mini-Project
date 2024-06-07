namespace MVC_Mini_Project.Models
{
    public class Social : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<InstructorSocial> InstructorSocials { get; set; }
    }
}
