using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Mini_Project.Models
{
    public class InstructorSocial
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Instructor))] public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        [ForeignKey(nameof(Social))] public int SocialId { get; set;}
        public Social Social { get; set; }
        public string Link { get; set; }
    }
}
