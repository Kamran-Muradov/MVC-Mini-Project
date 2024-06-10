using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Contacts
{
    public class ContactVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
