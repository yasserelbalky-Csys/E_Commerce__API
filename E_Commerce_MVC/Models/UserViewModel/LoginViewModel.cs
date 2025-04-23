using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.UserViewModel
{
    public class LoginViewModel
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
        [Display(Name = "Remember ME")]
        public bool RememberMe { get; set; }
    }
}