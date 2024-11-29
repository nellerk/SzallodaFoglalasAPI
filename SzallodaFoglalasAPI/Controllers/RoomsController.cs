using Microsoft.AspNetCore.Mvc;
using SzallodaFoglalasAPI.Context;
using SzallodaFoglalasAPI.Entities;

namespace SzallodaFoglalasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelContext _context;

        public RoomsController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllRooms() => Ok(_context.Rooms.ToList());

        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public IActionResult AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, Room updatedRoom)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();

            room.RoomNumber = updatedRoom.RoomNumber;
            room.Type = updatedRoom.Type;
            room.PricePerNight = updatedRoom.PricePerNight;
            room.IsAvailable = updatedRoom.IsAvailable;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
