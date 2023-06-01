using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PesaPap.Entities
{
    [Index(nameof(TransactionRef), IsUnique = true)]
    public class PaymentNotifications
    {
        public PaymentNotifications()
        {
            this.DateCreated = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [StringLength(maximumLength: 50, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? TransactionRef { get; set; }

        public DateTime PaymentDate { get; set; }
        [Required]

        public decimal Amount { get; set; }

        public Institution Institution { get; set; }

        public PaymentChannels Channel { get; set; }
        [MaxLength(100)]
        [StringLength(maximumLength: 100, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? StudentName { get; set; }
        [MaxLength(200)]
        [StringLength(maximumLength: 200, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? StudentNo { get; set; }
        [MaxLength(50)]
        [StringLength(maximumLength: 50, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? PaymentNarration { get; set; }
        [MaxLength(100)]
        [StringLength(maximumLength: 100, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? PayerName { get; set; }
        [MaxLength(50)]
        [StringLength(maximumLength: 50, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? AccountNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IPNSent { get; set; } = false;
        public DateTime ? DateTimeSent { get; set; }

        public int NoOfTries { get; set; } = 0;

        public DateTime? LastIPNRetryDate { get; set; }



    }
}
