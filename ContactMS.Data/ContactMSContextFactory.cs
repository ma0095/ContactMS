using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ContactMS.Data
{
    public class ContactMSContextFactory : IDesignTimeDbContextFactory<ContactMSContext>
    {
        public ContactMSContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ContactMS");

            var configuration = new ConfigurationBuilder().SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

            var connectionString = configuration.GetConnectionString("ContactDb");

            var optionsBuilder = new DbContextOptionsBuilder<ContactMSContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new ContactMSContext(optionsBuilder.Options);
        }
    }
}