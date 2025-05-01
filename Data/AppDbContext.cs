using ContactApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Representa la tabla "Contacts"
        public DbSet<Contact> Contacts { get; set; }
    }
}
