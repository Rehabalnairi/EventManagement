namespace EventManagement.DTO
{
    public class CreateEventDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int MaxAttendees { get; set; }
    }
}
