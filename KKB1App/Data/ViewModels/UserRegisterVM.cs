using System.ComponentModel.DataAnnotations;

namespace KKB1App.Data.ViewModels
{
    public class UserRegisterVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
