using System.ComponentModel.DataAnnotations;

namespace PesaPap.Entities
{
    public class IPNSettings
    {
        public IPNSettings() { }

        [Key]
        public int Id { get; set; }
        [Url(ErrorMessage = "Invalid URL")]
        [MaxLength(100)]
        [StringLength(maximumLength: 100, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? NotificationUrl { get; set; }
        [Url(ErrorMessage = "Invalid URL")]
        [MaxLength(50)]
        [StringLength(maximumLength: 100, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string? ValidationUrl { get; set; }

        [Required(ErrorMessage = "{0} must be provided")]
        [RegularExpression("SOAP|REST", ErrorMessage = "Invalid Channel")]
        [StringLength(maximumLength: 50, MinimumLength = 4,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? NotificationChannel { get; set; }
        [Required]
        public Institution Institution { get; set; }    

    }
}
