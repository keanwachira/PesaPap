using Microsoft.AspNetCore.Authentication;
using Moq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PesaPap.Entities
{
    public class Institution
    {
        public Institution()
        {
            this.DateCreated = DateTime.Now;
        }
       
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} must be provided")]
        [StringLength(maximumLength: 50, MinimumLength = 4,
        ErrorMessage = "{0} should have {1} maximum characters and {2} minimum characters")]
        public string ? InstitutionName { get; set; }
        public bool Active { get; set; }
        
        public DateTime DateCreated { get; set; }

    }
}
