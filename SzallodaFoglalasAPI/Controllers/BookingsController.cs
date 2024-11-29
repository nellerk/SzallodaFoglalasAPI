using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SzallodaFoglalasAPI.Context;
using SzallodaFoglalasAPI.Entities;

namespace SzallodaFoglalasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly HotelContext _context;

        public BookingsController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBookings()
            => Ok(_context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .ToList());

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var booking = _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .FirstOrDefault(b => b.Id == id);

            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public IActionResult AddBooking(Booking booking)
        {
            var room = _context.Rooms.Find(booking.RoomId);
            if (room == null || !room.IsAvailable)
                return BadRequest("The room is either not found or unavailable.");

            booking.Room = room;
            room.IsAvailable = false; // Mark room as unavailable for this booking

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, Booking updatedBooking)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();

            booking.GuestId = updatedBooking.GuestId;
            booking.RoomId = updatedBooking.RoomId;
            booking.CheckInDate = updatedBooking.CheckInDate;
            booking.CheckOutDate = updatedBooking.CheckOutDate;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefault(b => b.Id == id);

            if (booking == null) return NotFound();

            // Mark the associated room as available again
            booking.Room.IsAvailable = true;

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return NoContent();
        }
    }

}
