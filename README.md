Event Management API is a backend system built with .NET 7, Entity Framework Core, and SQL Server for managing events and attendees.
It supports creating events, registering attendees, retrieving event lists, and generating reports including weather forecasts for upcoming events.
The API also implements JWT authentication, Serilog logging, Async operations, and performance optimizations like Eager Loading to avoid N+1 problems.

Technologies Used:
.NET 7 / ASP.NET Core
C#
Entity Framework Core
SQL Server
AutoMapper (DTO mapping)
Serilog (Logging)
Swagger / OpenAPI (API documentation)
JWT Authentication
HttpClient (External API integration for weather forecasts)

Features:
Event Management
Create events with title, description, location, date, and maximum attendees.
Retrieve all events or filter by location/date.
Sort events by number of attendees.
Attendee Management
Register attendees for events.
Get attendees for a specific event.
Validate max capacity before registration.
Reporting
Fetch upcoming events (next 30 days) with attendee count.
Fetch weather forecast for event locations via Open-Meteo API.

Security:
JWT token-based authentication for secure endpoints.

Logging:
Uses Serilog to log information and errors.
Logs written to console and daily rolling log files.

Database:
Tables
Events: EventId, Title, Description, Date, Location, MaxAttendees
Attendees: AttendeeId, Name, Email, EventId, RegisteredAt

Relationships:
One Event → Many Attendees
Database Optimizations
Indexes on frequently searched fields (e.g., Location).
Eager Loading for Events with Attendees to avoid N+1 Problem.

API Endpoints:
Event Endpoints:
GET /api/event - Get all events with optional filters:
location (query), date (query), sortByAttendeeCount (boolean)
POST /api/event - Create a new event

Attendee Endpoints:
GET /api/attendee/event/{eventId} - Get attendees for an event
POST /api/attendee - Register attendee

Report Endpoints:
GET /api/report/upcoming - Get upcoming events with weather forecas
