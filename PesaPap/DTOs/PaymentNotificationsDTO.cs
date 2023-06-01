using PesaPap.Entities;
using System.ComponentModel.DataAnnotations;

namespace PesaPap.DTOs
{
    public class PaymentNotificationsDTO
    {       
       
        [MaxLength(50)]
        [StringLength(maximumLength: 50, MinimumLength = 5,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? TransactionRef { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required, Range(1, 1000000000)]

        public decimal Amount { get; set; }

        public int InstitutionId { get; set; }

        public int ChannelID { get; set; }
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
       
        


    }
}
