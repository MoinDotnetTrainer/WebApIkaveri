using System.ComponentModel.DataAnnotations;

namespace WebApIkaveri.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName Required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string? Password { get; set; }
    }
}
