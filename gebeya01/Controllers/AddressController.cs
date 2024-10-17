using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using gebeya01.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _addressRepository;
        private readonly IPerson _personRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public AddressController(IAddress addressRepository, IPerson personRepository, IMapper mapper, ApplicationDbContext context)
        {
            _addressRepository = addressRepository;
            _personRepository = personRepository;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AddressDto>))]
        public async Task<IActionResult> GetAddressesAsync(int userId)
        {
            var addresses = await _addressRepository.GetAdressesOfUserAsync(userId);

            var addressDtos = _mapper.Map<List<AddressDto>>(addresses);
            if (!addressDtos.Any())
            {
                return NotFound();
            }
            return Ok(addressDtos);

        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressDto addressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that the user exists
            var person = await _personRepository.GetPersonAsync(addressDto.UserID);
            
            if (person == null)
            {
                return NotFound("User not found");
            }
            

            var address = new Address
            {
                Person = person, // Set the Person navigation property directly
                StreetAddress = addressDto.StreetAddress,
                City = addressDto.City,
                ZIPCode = addressDto.ZIPCode,
                Country = addressDto.Country,
                IsDefault = addressDto.IsDefault,
                Region = addressDto.Region,
            };
            if (person.Address == null)
            {
                address.IsDefault = true;
            }
            if(address.IsDefault == true && person.Address != null)
            {
                var userID = address.Person.UserID;

                var prevDeafultAddress = await _context.Addresses.FirstOrDefaultAsync(w => w.UserID == userID && w.IsDefault == true);
                prevDeafultAddress.IsDefault = false;

                _context.Update(prevDeafultAddress);
                _context.SaveChangesAsync();
            }

            await _addressRepository.AddAddressAsync(address);
            CreatedAtAction(nameof(GetAddressesAsync), new { id = address.AddressID }, addressDto);
            return Ok();
        }
        [HttpDelete("{AddressID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAddressAsync(int AddressID)
        {
            try
            {
                var deleted = await _addressRepository.DeleteAddressAsync(AddressID);

                if (!deleted)
                {
                    return NotFound(); // Product not found
                }

                return NoContent(); // Successfully deleted
            }
            catch (Exception ex)
            {
                // Log the exception
                //_logger.LogError(ex, "An error occurred while deleting the product.");
                return StatusCode(500, "A problem occurred while handling your request.");
            }

        }




        }
}
