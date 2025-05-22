using System.ComponentModel.DataAnnotations;

namespace KKB1App.Data.ViewModels
{
    public class UserVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
