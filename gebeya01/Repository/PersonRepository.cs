using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace gebeya01.Repository
{
    public class PersonRepository : IPerson

    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PersonRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ICollection<Person>> GetAllPersonsAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetPersonAsync(int UserID)
        {
            return await _context.Persons.FindAsync(UserID);
        }

        public async Task<Person> GetPersonByEmailAsync(string email)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<ICollection<Person>> GetPersonsByRoleAsync(string role)
        {
            return await _context.Persons.Where(p => p.Role == role).ToListAsync();
        }

        public async Task<bool> PersonExistsAsync(int userId)
        {
            return  await _context.Persons.AnyAsync(p => p.UserID == userId);
        }
        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId)
        {
            var person = await _context.Persons.FindAsync(userId);
            var role = person.Role;

            if (person != null && role != "Admin" && role != "admin")
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePartialAsync(int userId, PersonDto personDto)
        {
            var person = await _context.Persons.FindAsync(userId);
            if (person == null)
                throw new KeyNotFoundException("Person not found");
            if (personDto.UserID != null)
                person.UserID = personDto.UserID;
            // Update only the fields provided in the DTO
            if (personDto.FirstName != null)
                person.FirstName = personDto.FirstName;

            if (personDto.LastName != null)
                person.LastName = personDto.LastName;

            if (personDto.Email != null)
                person.Email = personDto.Email;

            if (!string.IsNullOrEmpty(personDto.Password))
            {
                // Hash the new password and update the password hash and salt
                CreatePasswordHash(personDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                person.PasswordHash = Convert.ToBase64String(passwordHash);
                person.PasswordSalt = Convert.ToBase64String(passwordSalt);
            }

            if (personDto.PhoneNumber != null)
                person.PhoneNumber = personDto.PhoneNumber;

            if (!string.IsNullOrEmpty(personDto.Role))
                person.Role = personDto.Role;

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        
    }
}

