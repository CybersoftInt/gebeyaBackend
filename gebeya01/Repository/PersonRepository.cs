using AutoMapper;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
