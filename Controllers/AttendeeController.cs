namespace EventManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Services;
using EventManagement.Models;
using EventManagement.DTO;

[ApiController]
[Route("api/[controller]")]
public class AttendeeController : ControllerBase
{
    
    private readonly AttendeeService _attendeeService;

    public AttendeeController(AttendeeService attendeeService)
    {
        _attendeeService = attendeeService;
    }
  


    // GET /api/attendee/event/1
    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetAttendeesByEvent(int eventId)
    {
        var attendees = await NewMethod(eventId);
        return Ok(attendees);

        Task<List<Attendee>> NewMethod(int eventId)
        {
            return _attendeeService.GetAttendeesByEventIdAsync(eventId);
        }
    }

    // POST /api/attendee
    [HttpPost]
    public async Task<IActionResult> RegisterAttendee([FromBody] CreateAttendeeDto dto)
    {
        try
        {
            var attendee = await _attendeeService.RegisterAttendeeAsync(dto);
            return CreatedAtAction(nameof(GetAttendeesByEvent), new { eventId = attendee.EventId }, attendee);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
