using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TraineesApp.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "You Have To Choose A Gender")]
        public Gender Gender { get; set; }


    }
}
