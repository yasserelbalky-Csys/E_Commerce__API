using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.UserViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}