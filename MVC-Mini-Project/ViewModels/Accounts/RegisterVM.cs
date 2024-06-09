using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Accounts
{
    public class RegisterVM
    {
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^(?i)[a-z]+[\ -].*[a-z]$", ErrorMessage = "Full name format is wrong")]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
