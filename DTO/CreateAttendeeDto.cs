namespace EventManagement.DTO
{
    public class CreateAttendeeDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public int EventId { get; set; }
    }
}
