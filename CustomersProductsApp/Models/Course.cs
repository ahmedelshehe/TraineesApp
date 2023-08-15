using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineesApp.Models
{
	public class Course
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(10, ErrorMessage = "Topic Can not be less that 10 letters"), MaxLength(50, ErrorMessage = "Topic Can Not be more than 50 letters")]
		public string Topic { get; set; }

		[Required(ErrorMessage = "You Must Enter A Grade")]
		[Range(0, int.MaxValue, ErrorMessage = "Grade Cannot Be Negative")]
		public int? Grade { get; set; }
		[Required(ErrorMessage = "You Must Choose A Track")]
		[ForeignKey("Track")]
		public int? TrackId { get; set; }

		public virtual Track? Track { get; set; }
	}
}
