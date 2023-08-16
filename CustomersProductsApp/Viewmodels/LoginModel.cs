using System.ComponentModel.DataAnnotations;

namespace TraineesApp.Viewmodels
{
    public class LoginModel
    {
        [Key]
        public string? Id { get; set; } = null;
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
