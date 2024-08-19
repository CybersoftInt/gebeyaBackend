using Microsoft.AspNetCore.Mvc;
using gebeya01.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Persons.Include(p => p.Address).ToListAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Persons
                .Include(p => p.Address)  // Include the related Address entity
                .FirstOrDefaultAsync(p => p.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            var userWithAddress = new
            {
                user.UserID,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Password,
                user.PhoneNumber,
                user.Role,
                Address = user.Address  // Directly use the navigation property
            };

            return Ok(userWithAddress);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure the address exists before adding the user
            var address = await _context.Addresses.FindAsync(person.AddressID);
            if (address == null)
            {
                return BadRequest("Address not found.");
            }

            // Add user
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = person.UserID }, person);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] Person person)
        {
            if (id != person.UserID)
            {
                return BadRequest();
            }

            var existingUser = await _context.Persons
                .Include(p => p.Address)  // Include the related Address entity
                .FirstOrDefaultAsync(p => p.UserID == id);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Ensure the address exists before updating
            var address = await _context.Addresses.FindAsync(person.AddressID);
            if (address == null)
            {
                return BadRequest("Address not found.");
            }

            // Update user information
            existingUser.FirstName = person.FirstName;
            existingUser.LastName = person.LastName;
            existingUser.Email = person.Email;
            existingUser.Password = person.Password;
            existingUser.PhoneNumber = person.PhoneNumber;
            existingUser.Role = person.Role;
            existingUser.AddressID = person.AddressID; // Update address ID

            _context.Entry(existingUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Persons.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.UserID == id);
        }
    }
}
