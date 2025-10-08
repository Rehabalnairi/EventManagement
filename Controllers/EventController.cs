namespace EventManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Services;
using EventManagement.Models;
using EventManagement.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }

    // GET /api/event?location=NY&date=2025-10-20&sortByAttendeeCount=true
    [HttpGet]
    public async Task<IActionResult> GetEvents([FromQuery] string? location, [FromQuery] DateTime? date, [FromQuery] bool sortByAttendeeCount = false)
    {
        var events = await _eventService.GetEventsAsync(location, date, sortByAttendeeCount);
        return Ok(events);
    }

    // POST /api/event
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto dto)
    {
        try
        {
            var ev = await _eventService.CreateEventAsync(dto);
            return CreatedAtAction(nameof(GetEvents), new { id = ev.EventId }, ev);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}