using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using gebeya01.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPerson _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPerson personRepository, IMapper mapper)
        {

            _personRepository = personRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public async Task<IActionResult> GetPersonGetAllPersonsAsync()
        {
            var persons = await _personRepository.GetAllPersonsAsync();
            var personDtos = _mapper.Map<List<PersonDto>>(persons);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(personDtos);
        }
        [HttpGet("{UserID:int}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPersonAsync(int UserID)
        {
            if (!await _personRepository.PersonExistsAsync(UserID))
                return NotFound();

            var person = await _personRepository.GetPersonAsync(UserID);
            var personDtos = _mapper.Map<PersonDto>(person);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(personDtos);
        }
        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        public async Task<IActionResult> GetPersonByEmailAsync(string email)
        {

            var Person = await _personRepository.GetPersonByEmailAsync(email);
            if (Person == null)
                return NotFound();
            var personDto = _mapper.Map<PersonDto>(Person);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(personDto);
        }
        [HttpGet("role/{role}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        public async Task<IActionResult> GetPersonsByRoleAsync(string role)
        {

            var Person = await _personRepository.GetPersonsByRoleAsync(role);
            if (Person == null)
                return NotFound();
            var personDto = _mapper.Map<List<PersonDto>>(Person);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(personDto);
        }
        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdatePersonAsync(int userId, [FromBody] PersonDto personDto)
        {
            if (userId != personDto.UserID)
                return BadRequest("User ID mismatch");

            if (!await _personRepository.PersonExistsAsync(userId))
                return NotFound();

            var person = _mapper.Map<Person>(personDto);
            await _personRepository.UpdateAsync(person);

            return NoContent();
        }
        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeletePersonAsync(int userId)
        {
            if (!await _personRepository.PersonExistsAsync(userId))
                return NotFound();

            await _personRepository.DeleteAsync(userId);
            return NoContent();
        }
        [HttpPatch("{userId:int}")]
        public async Task<IActionResult> UpdatePersonPartialAsync(int userId, [FromBody] PersonDto personDto)
        {
            if (userId != personDto.UserID)
                return BadRequest("User ID mismatch");

            if (!await _personRepository.PersonExistsAsync(userId))
                return NotFound();

            try
            {
                await _personRepository.UpdatePartialAsync(userId, personDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
