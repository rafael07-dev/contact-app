using ContactApp.Models;

namespace ContactApp.Interfaces
{
    public interface IContactRepository
    {
        Task<PaginatedList<Contact>> GetContactAsync(int pageIndex, int pageSize);
        Task<Contact> GetByIdAsync(int id);
        Task<Contact> AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(Contact contact);
    }
}
