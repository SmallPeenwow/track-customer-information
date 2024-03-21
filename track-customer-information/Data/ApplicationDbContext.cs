using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using track_customer_information.Models;

namespace track_customer_information.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
    }
}
