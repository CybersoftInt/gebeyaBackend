using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface IAddress
    {
        Task<IEnumerable<Address>> GetAdressesOfUserAsync(int userID);
        Task PreviousDeafultAddress();
        Task AddAddressAsync(Address address);
        Task<bool> DeleteAddressAsync(int AddressID);
    }
}
