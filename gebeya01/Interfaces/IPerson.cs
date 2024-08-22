using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface IPerson
    {
        Task<ICollection<Person>> GetAllPersonsAsync();
        Task<Person> GetPersonAsync(int UserID);
        Task<Person> GetPersonByEmailAsync(string email);
        Task<bool> PersonExistsAsync(int userId);
        Task<ICollection<Person>> GetPersonsByRoleAsync(string role);

    }
}
