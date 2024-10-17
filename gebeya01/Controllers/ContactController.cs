// Controllers/ContactController.cs
using Microsoft.AspNetCore.Mvc;
using gebeya01.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SendContactForm([FromBody] ContactForm contactForm)
    {
        if (contactForm == null || !ModelState.IsValid)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            _context.contactForms.Add(contactForm);
            await _context.SaveChangesAsync();

            // Optionally send an email here

            return Ok("Message sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpGet]
    public async Task<IActionResult> showContactMessages(int userId)
    {
        var message =  await _context.contactForms.FindAsync(userId);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(message);
    }
}
