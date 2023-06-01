using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace PesaPap.Entities
{
    public class PaymentChannels
    {
        public PaymentChannels()
        {
            this.DateCreated = DateTime.Now;  
        }


        [Key]
        public int ChannelId { get; set; }
        [Required(ErrorMessage = "{0} must be provided"), MaxLength(50)]
        [StringLength(maximumLength: 50, MinimumLength = 3,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? ChannelName { get; set; }

        public DateTime ? DateCreated { get; set; }
       

    }
}
