
using System.ComponentModel.DataAnnotations;
namespace EventManagement.Models

{
    public class Event
    { 
        [Key]
        public int EventId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        [Required]
        [Range(10, 500)]
        public int MaxAttendees { get; set; }

        public List<Attendee> Attendees { get; set; } = new List<Attendee>();

    }
}
