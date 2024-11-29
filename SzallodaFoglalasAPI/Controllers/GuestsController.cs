using Microsoft.AspNetCore.Mvc;
using SzallodaFoglalasAPI.Context;
using SzallodaFoglalasAPI.Entities;

namespace SzallodaFoglalasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly HotelContext _context;

        public GuestsController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllGuests() => Ok(_context.Guests.ToList());

        [HttpGet("{id}")]
        public IActionResult GetGuest(int id)
        {
            var guest = _context.Guests.Find(id);
            if (guest == null) return NotFound();
            return Ok(guest);
        }

        [HttpPost]
        public IActionResult AddGuest(Guest guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetGuest), new { id = guest.Id }, guest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGuest(int id, Guest updatedGuest)
        {
            var guest = _context.Guests.Find(id);
            if (guest == null) return NotFound();

            guest.Name = updatedGuest.Name;
            guest.Email = updatedGuest.Email;
            guest.PhoneNumber = updatedGuest.PhoneNumber;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGuest(int id)
        {
            var guest = _context.Guests.Find(id);
            if (guest == null) return NotFound();

            _context.Guests.Remove(guest);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
