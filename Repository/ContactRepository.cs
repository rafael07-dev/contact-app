using ContactApp.Data;
using ContactApp.Interfaces;
using ContactApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task DeleteAsync(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync() => await _context.Contacts.ToListAsync();

        public async Task<Contact> GetByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                throw new Exception("Contact not found");
            }

            return contact;

        }
        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
