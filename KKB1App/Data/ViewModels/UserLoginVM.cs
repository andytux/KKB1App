using System.ComponentModel.DataAnnotations;

namespace KKB1App.Data.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
