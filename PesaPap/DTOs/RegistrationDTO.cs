using System.ComponentModel.DataAnnotations;

namespace PesaPap.DTOs
{
    public class RegistrationDTO
    {
        

        [Required, EmailAddress(ErrorMessage = "Valid Email is required")]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        [StringLength(maximumLength: 100, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string Password { get; set; }
       
    }
}
