using ContactApp.Interfaces;
using ContactApp.Models;

namespace ContactApp.Services
{
    public class ContactService: IContactService
    {
        private readonly IContactRepository _repo;

        public ContactService(IContactRepository repo)
        {
            _repo = repo;
        }

        public async Task<Contact> CreateAsync(Contact entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);

            if (c != null)
            {
                await _repo.DeleteAsync(c);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var contacts = await _repo.GetAllAsync();

            return contacts;
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);

            if (c == null) return null;

            return c;
        }

        public async Task UpdateAsync(int id, Contact entity)
        {
            var contactSaved = await _repo.GetByIdAsync(id);
            if (contactSaved == null) return;

            contactSaved.Name = entity.Name;
            contactSaved.Email = entity.Email;
            contactSaved.Phone = entity.Phone;
            contactSaved.City = entity.City;
            contactSaved.Address = entity.Address;
            contactSaved.PhotoUrl = entity.PhotoUrl;

            await _repo.UpdateAsync(contactSaved);
        }
    }
}
