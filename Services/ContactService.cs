using ContactApp.Interfaces;
using ContactApp.Models;

namespace ContactApp.Services
{
    public class ContactService: IContactService
    {
        private readonly IContactRepository _repo;
        private readonly IFileService _fileService;
        private ILogger<ContactService> _logger;

        public ContactService(IContactRepository repo, IFileService fileService, ILogger<ContactService> logger)
        {
            _repo = repo;
            _fileService = fileService;
            _logger = logger;   
        }

        public async Task<Contact> CreateAsync(Contact entity)
        {  
            return await _repo.AddAsync(entity);
        }

        public async Task<Contact> DeleteAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);

            await _repo.DeleteAsync(c);

            var fileName = GetFileName(c.PhotoUrl);

            _fileService.DeleteFile(fileName);

            return c;
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, Contact entity)
        {
            var contactSaved = await _repo.GetByIdAsync(id);

            contactSaved.Name = entity.Name;
            contactSaved.Email = entity.Email;
            contactSaved.Phone = entity.Phone;
            contactSaved.City = entity.City;
            contactSaved.Address = entity.Address;
            contactSaved.PhotoUrl = entity.PhotoUrl;

            await _repo.UpdateAsync(contactSaved);
        }

        public async Task<PaginatedList<Contact>> GetContactAsync(int pageIndex, int pageSize)
        {
            var contacts = await _repo.GetContactAsync(pageIndex, pageSize);
            return contacts;
        }

        public async Task<PaginatedList<Contact>> GetContactBySearchAsync(string search, int pageIndex, int pageSize)
        {
            return await _repo.GetContactBySearchAsync(search, pageIndex, pageSize);
        }

        public string GetFileName(string fileName)
        {
            string[] file = fileName.Split("/");

            return file[2];
        }
    }
}
