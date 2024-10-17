using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Repository
{
    public class AddressRepository : IAddress
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAddressAsync(int AddressID)
        {
            var addres = await _context.Addresses.FindAsync(AddressID);
            if (addres == null)
            {
                return false; // Product not found
            }

            _context.Addresses.Remove(addres);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log exception or handle it accordingly
                return false;
            }
        }

        public async Task<IEnumerable<Address>> GetAdressesOfUserAsync(int userID)
        {
           return await _context.Addresses.Where(w => w.Person.UserID == userID).Include(w => w.Person.Address).ToListAsync();
        }

        public Task PreviousDeafultAddress()
        {
            throw new NotImplementedException();
        }
    }
}
