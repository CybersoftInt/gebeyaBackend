// src/Controllers/AuthController.cs
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPerson _personRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IPerson personRepository, IConfiguration configuration)
        {
            _personRepository = personRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Person>> Register(UserDto request)
        {
            // Ensure all required fields are present
            if (string.IsNullOrEmpty(request.FirstName))
            {
                return BadRequest("First name is required.");
            }

            // Hash the password
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName, // Handle optional last name
                Email = request.Username,
                PasswordHash = Convert.ToBase64String(passwordHash), // Store base64 string of password hash
                PasswordSalt = Convert.ToBase64String(passwordSalt) // Store base64 string of password salt
            };

            try
            {
                // Save person to the database
                await _personRepository.AddAsync(person);
                return Ok(person);
            }
            catch (Exception ex)
            {
                // Log the error and return a proper error response
                Console.WriteLine($"Error during registration: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            // Find person by email (used as username here)
            var person = await _personRepository.GetPersonByEmailAsync(request.Username);

            if (person == null)
            {
                return BadRequest("User not found.");
            }

            // Decode stored password hash and salt
            var storedHash = Convert.FromBase64String(person.PasswordHash);
            var storedSalt = Convert.FromBase64String(person.PasswordSalt);

            if (!VerifyPasswordHash(request.Password, storedHash, storedSalt))
            {
                return BadRequest("Wrong password.");
            }

            // Generate JWT token
            var token = CreateToken(person);
            return Ok(token);
        }

        private string CreateToken(Person person)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, person.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
