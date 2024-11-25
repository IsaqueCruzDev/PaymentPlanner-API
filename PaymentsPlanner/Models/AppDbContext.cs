using Microsoft.EntityFrameworkCore;

namespace PaymentsPlanner.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Payment> Payment { get; set; } = null!;
        public DbSet<PaymentType> PaymentType { get; set; } = null!;
    }
}
