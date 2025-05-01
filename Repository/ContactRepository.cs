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

        //public async Task<IEnumerable<Contact>> GetAllAsync() => await _context.Contacts.ToListAsync();

        public async Task<Contact> GetByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                throw new Exception("Contact not found");
            }

            return contact;

        }

        public async Task<PaginatedList<Contact>> GetContactAsync(int pageIndex, int pageSize)
        {
            var contacts = await _context.Contacts
                .OrderBy(x => x.Id)
                .Skip((pageIndex -1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.Contacts.CountAsync();

            var totalpages = (int) Math.Ceiling(count / (double)pageSize);

            return new PaginatedList<Contact> (contacts, pageIndex, totalpages);
        }

        public async Task<PaginatedList<Contact>> GetContactBySearchAsync(string search, int pageIndex, int pageSize)
        {
            var query = _context.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(search) ||
                                 c.Email.ToLower().Contains(search) ||
                                 c.Phone.Contains(search));
            }

            int totalCount = await query.CountAsync();
            var totalpages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var contacts = await query
                .OrderBy(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<Contact>(contacts, pageIndex, totalpages);
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
