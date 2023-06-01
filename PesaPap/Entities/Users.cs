using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace PesaPap.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress(ErrorMessage ="Valid Email is required")]
        public string Email{ get; set; }
        [Required]        
        [Column(TypeName = "varchar(MAX)")]
        public string Password { get; set; }
        public string Salt { get; set; }

    }
}
