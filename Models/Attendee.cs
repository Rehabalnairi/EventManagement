
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models
{
    public class Attendee
    {

        [Key]
        public int AttendeeId { get; set; }
        [Required]
        [MaxLength(80)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Phone { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
        //foreign key
        public int EventId { get; set; }
        public Event Event { get; set; }= null!;






    }
}