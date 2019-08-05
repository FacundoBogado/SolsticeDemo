using System.Data.Entity;

namespace SolsticeDemo.Models
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}
