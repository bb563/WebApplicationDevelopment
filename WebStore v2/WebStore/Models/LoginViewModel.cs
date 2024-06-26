using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
