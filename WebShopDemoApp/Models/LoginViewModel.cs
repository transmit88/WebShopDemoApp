using System.ComponentModel.DataAnnotations;

namespace WebShopDemoApp.Models
{
    public class LoginViewModel
    {

        [Required]
        public string Email { get; set; } = null!;


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }
    }
}
