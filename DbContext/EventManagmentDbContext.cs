using Microsoft.EntityFrameworkCore;
using EventManagement.Models;
using System;

namespace EventManagement.DbContext
{
    public class EventManagmentDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public EventManagmentDbContext(DbContextOptions<EventManagmentDbContext> options) :
            base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Attendee> Attendees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attendee>()
                .HasIndex(a => a.Email).IsUnique();

            // Seeding two sample events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    Title = "Tech Conference 2024",
                    Description = "A conference about the latest in technology.",
                    Date = DateTime.UtcNow.AddDays(10),
                    Location = "New York City",
                    MaxAttendees = 200
                },
                new Event
                {
                    EventId = 2,
                    Title = "Health & Wellness Expo",
                    Description = "An expo focused on health and wellness products and services.",
                    Date = DateTime.UtcNow.AddDays(25),
                    Location = "Los Angeles",
                    MaxAttendees = 150
                }
            );
        }
    }
}
