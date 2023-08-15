using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineesApp.Models
{
    public class Trainee
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(25, ErrorMessage = "Name Must Be Less Than 25 character"), MinLength(10, ErrorMessage = "Name Must Be More Than 10 character")]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "You Have To Choose A Gender")]
        public Gender? Gender { get; set; }

        [Required]
        [DisplayName("Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [DisplayName("Mobile Number")]
        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01[0-2]{1}[0-9]{8}$", ErrorMessage = "Invalid phone number")]
        public string MobileNumber { get; set; }

        [ForeignKey("Track")]
        public int? TrackId { get; set; }
        public virtual Track? Track { get; set; }
    }
    public enum Gender
    { Male, Female }
}
