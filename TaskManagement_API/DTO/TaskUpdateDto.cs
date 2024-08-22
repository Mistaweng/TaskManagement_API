using System.ComponentModel.DataAnnotations;

namespace TaskManagement_API.DTO
{
	public class TaskUpdateDto
	{
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}
