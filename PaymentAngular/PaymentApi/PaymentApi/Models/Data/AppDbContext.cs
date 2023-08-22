using Microsoft.EntityFrameworkCore;

namespace PaymentApi.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
        public DbSet<PaymentDetails> paymentDetails { get; set; }
    }
}
