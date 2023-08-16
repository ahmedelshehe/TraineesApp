using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TraineesApp.Models;

namespace TraineesApp.Viewmodels
{
    public class RegisterModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "You Have To Choose A Gender")]
        public Gender Gender { get; set; }
    }
}
