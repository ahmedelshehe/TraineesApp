using System.ComponentModel.DataAnnotations;

namespace TraineesApp.Models
{
    public class Track
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Name Can not be less that 10 letters"), MaxLength(50, ErrorMessage = "Name Can Not be more than 50 letters")]
        public string Name { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Description Can not be less that 20 letters"), MaxLength(100, ErrorMessage = "Description Can Not be more than 100 letters")]
        public string Description { get; set; }

        public virtual ICollection<Course>? Courses { get; set; }
        public virtual ICollection<Trainee>? Trainees { get; set; }
    }
}
