using ContactMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Mappings
{
    internal class ContactDetailMap : IEntityTypeConfiguration<ContactDetail>
    {
        public void Configure(EntityTypeBuilder<ContactDetail> builder)
        {
            _ = builder.ToTable("ContactDetails");
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.ContactId).IsRequired();
            _ = builder.Property(x => x.ContactNumber).IsRequired();
            _ = builder.HasOne(x => x.Contact)             
       .WithMany(x => x.ContactDetails).HasForeignKey(x => x.ContactId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
