

using ContactMS.Data.Entities;
using ContactMS.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ContactMS.Data
{
    public class ContactMSContext : DbContext
    {
        public ContactMSContext()
        {
        }

        public ContactMSContext(DbContextOptions<ContactMSContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263. 
            _ = optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ContactMS;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //for fluent API approach and also we can create a separate mapper for each entity
            #region Mapping
            _ = modelBuilder.ApplyConfiguration(new ContactMap());


            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}