using ContactApp.Models;

namespace ContactApp.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task<Contact> CreateAsync(Contact entity);
        Task UpdateAsync(int id, Contact entity);
        Task DeleteAsync(int id);
    }
}
