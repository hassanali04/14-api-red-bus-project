using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Paaword is Required")]
        public string? Password { get; set; }
    }
}
