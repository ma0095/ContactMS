using ContactMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactMS.Data.Mappings
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            _ = builder.ToTable("Contacts");
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            _ = builder.Property(x => x.ActiveStatus).IsRequired();
        }
    }
}
